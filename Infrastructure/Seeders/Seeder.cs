using Domain.Constants;
using Domain.Entites;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Seeders
{
    internal sealed class Seeder : ISeeder
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seeder(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            if (await _context.Database.CanConnectAsync())
            {
                if (!_context.Plans.Any())
                {
                    var features = GetFeatures();
                    await _context.Features.AddRangeAsync(features);
                    var plans = GetPlans(features);
                    await _context.Plans.AddRangeAsync(plans);
                    await _context.SaveChangesAsync();
                }

                // Seed permissions (idempotent on empty table)
                if (!_context.permissions.Any())
                {
                    var permissions = GetPermissions();
                    await _context.permissions.AddRangeAsync(permissions);
                    await _context.SaveChangesAsync();
                }

                if (!await _context.Roles.AnyAsync())
                {
                    var ownerRole = new IdentityRole
                    {
                        Name = RolesConstants.Owner,
                        NormalizedName = RolesConstants.Owner.ToUpper()
                    };
                    var assistantRole = new IdentityRole
                    {
                        Name = RolesConstants.Assistant,
                        NormalizedName = RolesConstants.Assistant.ToUpper()
                    };
                    await _roleManager.CreateAsync(ownerRole);
                    await _roleManager.CreateAsync(assistantRole);
                }
            }
        }

        private List<Feature> GetFeatures()
        {
            var seedDate = DateTime.UtcNow;

            return new List<Feature>
                {
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "عدد الطلاب",
                        Description = "إدارة حتى 100 طالب في المنصة.",
                        Key = "student_limit_basic",
                        CreatedAt = seedDate,
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "عدد الدروس",
                        Description = "إنشاء ما يصل إلى 50 درسًا.",
                        Key = "lesson_limit_basic",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "الاختبارات والواجبات",
                        Description = "أنشئ اختبارات وواجبات بسيطة لتقييم الطلاب.",
                        Key = "quiz_assignment_basic",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "دعم فني أساسي",
                        Description = "دعم عبر البريد الإلكتروني خلال أيام العمل.",
                        Key = "support_basic",
                        CreatedAt = seedDate
                    },

                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "عدد الطلاب",
                        Description = "إدارة حتى 500 طالب.",
                        Key = "student_limit_growth",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "عدد الدروس والدورات",
                        Description = "إنشاء حتى 200 درس و10 دورات متكاملة.",
                        Key = "lesson_limit_growth",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "الاختبارات الذكية",
                        Description = "توليد اختبارات تلقائيًا باستخدام الذكاء الاصطناعي.",
                        Key = "ai_quiz_generation_growth",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "التحليلات والتقارير",
                        Description = "احصل على تقارير مفصلة حول تقدم الطلاب وتفاعلهم.",
                        Key = "analytics_growth",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "دعم فني متقدم",
                        Description = "دعم سريع عبر البريد الإلكتروني والدردشة.",
                        Key = "support_growth",
                        CreatedAt = seedDate
                    },

                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "عدد الطلاب ",
                        Description = "إدارة عدد 1000 من الطلاب.",
                        Key = "student_limit_pro",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "عدد الدروس والدورات غير محدود",
                        Description = "أنشئ عددًا غير محدود من الدروس والدورات.",
                        Key = "lesson_limit_pro",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "جلسات بث مباشر",
                        Description = "قم ببث محاضرات مباشرة والتفاعل مع الطلاب في الوقت الفعلي.",
                        Key = "live_sessions_pro",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "موقع مخصص للأكاديمية",
                        Description = "أنشئ موقعك التعليمي الكامل بعلامتك التجارية الخاصة.",
                        Key = "custom_website_pro",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "ذكاء اصطناعي متقدم",
                        Description = "ميزات AI لإنشاء الدروس، الكورسات، والأسئلة تلقائيًا.",
                        Key = "ai_features_pro",
                        CreatedAt = seedDate
                    },
                    new Feature
                    {
                        Id = Guid.NewGuid(),
                        Name = "دعم فني على مدار الساعة",
                        Description = "دعم متميز متوفر 24/7 عبر جميع القنوات.",
                        Key = "support_pro",
                        CreatedAt = seedDate
                    }
                };
        }

        private List<Plan> GetPlans(List<Feature> features)
        {
            var baseDate = DateTime.UtcNow;

            var studentLimitFeatureBasic = features.First(f => f.Key == "student_limit_basic");
            var lessonLimitFeatureBasic = features.First(f => f.Key == "lesson_limit_basic");
            var quizAssignmentFeatureBasic = features.First(f => f.Key == "quiz_assignment_basic");
            var supportFeatureBasic = features.First(f => f.Key == "support_basic");

            var studentLimitFeatureGrowth = features.First(f => f.Key == "student_limit_growth");
            var lessonLimitFeatureGrowth = features.First(f => f.Key == "lesson_limit_growth");
            var aiQuizFeatureGrowth = features.First(f => f.Key == "ai_quiz_generation_growth");
            var analyticsFeatureGrowth = features.First(f => f.Key == "analytics_growth");
            var supportFeatureGrowth = features.First(f => f.Key == "support_growth");

            var studentLimitFeaturePro = features.First(f => f.Key == "student_limit_pro");
            var lessonLimitFeaturePro = features.First(f => f.Key == "lesson_limit_pro");
            var liveSessionsFeaturePro = features.First(f => f.Key == "live_sessions_pro");
            var customWebsiteFeaturePro = features.First(f => f.Key == "custom_website_pro");
            var aiFeaturesFeaturePro = features.First(f => f.Key == "ai_features_pro");
            var supportFeaturePro = features.First(f => f.Key == "support_pro");

            return new List<Plan>
                {
                    new Plan
                    {
                        Id = Guid.NewGuid(),
                        Name = "الخطة الأساسية",
                        Slug = "basic",
                        Description = "ابدأ رحلتك التعليمية بالأدوات الأساسية لإنشاء الدروس وإدارة الطلاب بسهولة.",
                        CreatedAt = baseDate,
                        PlanPricings = new List<PlanPricing>
                        {
                            new PlanPricing
                            {
                                Id = Guid.NewGuid(),
                                Price = 1200m,
                                Currency = "EGP",
                                BillingCycle = BillingCycle.Monthly,
                                DiscountPercent = 0,
                                CreatedAt = baseDate
                            },
                            new PlanPricing
                            {
                                Id = Guid.NewGuid(),
                                Price = 13000m,
                                Currency = "EGP",
                                BillingCycle = BillingCycle.Annually,
                                DiscountPercent = 10,
                                CreatedAt = baseDate
                            }
                        },
                        PlanFeatures = new List<PlanFeature>
                        {
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = studentLimitFeatureBasic.Id,
                                LimitValue = 100,
                                LimitUnit = "طالب"
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = lessonLimitFeatureBasic.Id,
                                LimitValue = 50,
                                LimitUnit = "درس"
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = quizAssignmentFeatureBasic.Id,
                                LimitValue = 1,
                                LimitUnit = "أساسي"
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = supportFeatureBasic.Id,
                                LimitValue = 1,
                                LimitUnit = "أساسي"
                            }
                        }
                    },

                    new Plan
                    {
                        Id = Guid.NewGuid(),
                        Name = "خطة النمو",
                        Slug = "growth",
                        Description = "طور أكاديميتك مع مزيد من الإمكانيات والمرونة. تشمل جميع ميزات الخطة الأساسية.",
                        CreatedAt = baseDate,
                        PlanPricings = new List<PlanPricing>
                        {
                            new PlanPricing
                            {
                                Id = Guid.NewGuid(),
                                Price = 2990m,
                                Currency = "EGP",
                                BillingCycle = BillingCycle.Monthly,
                                DiscountPercent = 0,
                                CreatedAt = baseDate
                            },
                            new PlanPricing
                            {
                                Id = Guid.NewGuid(),
                                Price = 29900m,
                                Currency = "EGP",
                                BillingCycle = BillingCycle.Annually,
                                DiscountPercent = 15,
                                CreatedAt = baseDate
                            }
                        },
                        PlanFeatures = new List<PlanFeature>
                        {
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = studentLimitFeatureGrowth.Id,
                                LimitValue = 500,
                                LimitUnit = "طالب",
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = lessonLimitFeatureGrowth.Id,
                                LimitValue = 200,
                                LimitUnit = "درس",
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = aiQuizFeatureGrowth.Id,
                                LimitValue = 1,
                                LimitUnit = "نشط",
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = analyticsFeatureGrowth.Id,
                                LimitValue = 1,
                                LimitUnit = "نشط",
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = supportFeatureGrowth.Id,
                                LimitValue = 1,
                                LimitUnit = "متقدم",
                            }
                        }
                    },

                    new Plan
                    {
                        Id = Guid.NewGuid(),
                        Name = "الخطة الاحترافية",
                        Slug = "pro",
                        Description = "أقصى قدر من المرونة والميزات المخصصة. تشمل جميع ميزات خطط النمو والأساسية.",
                        CreatedAt = baseDate,
                        PlanPricings = new List<PlanPricing>
                        {
                            new PlanPricing
                            {
                                Id = Guid.NewGuid(),
                                Price = 6990m,
                                Currency = "EGP",
                                BillingCycle = BillingCycle.Monthly,
                                DiscountPercent = 0,
                                CreatedAt = baseDate
                            },
                            new PlanPricing
                            {
                                Id = Guid.NewGuid(),
                                Price = 69900m,
                                Currency = "EGP",
                                BillingCycle = BillingCycle.Annually,
                                DiscountPercent = 20,
                                CreatedAt = baseDate
                            }
                        },
                        PlanFeatures = new List<PlanFeature>
                        {
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = studentLimitFeaturePro.Id,
                                LimitValue = 1000,
                                LimitUnit = "طالب",
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = lessonLimitFeaturePro.Id,
                                LimitValue = -1,
                                LimitUnit = "غير محدود",
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = liveSessionsFeaturePro.Id,
                                LimitValue = 1,
                                LimitUnit = "نشط",
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = customWebsiteFeaturePro.Id,
                                LimitValue = 1,
                                LimitUnit = "موقع",
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = aiFeaturesFeaturePro.Id,
                                LimitValue = 1,
                                LimitUnit = "نشط",
                            },
                            new PlanFeature
                            {
                                Id = Guid.NewGuid(),
                                FeatureId = supportFeaturePro.Id,
                                LimitValue = 1,
                                LimitUnit = "متميز",
                            }
                        }
                    }
                };
        }

        private List<Permission> GetPermissions()
        {
            return new List<Permission>
                {
                    // Courses
                    new Permission { Id = "VIEW_COURSES", Name = "عرض الدورات", Description = "السماح بعرض جميع الدورات", Module = "courses" },
                    new Permission { Id = "CREATE_COURSES", Name = "إنشاء دورة", Description = "السماح بإنشاء دورات جديدة", Module = "courses" },
                    new Permission { Id = "EDIT_COURSES", Name = "تعديل الدورات", Description = "السماح بتعديل معلومات الدورة", Module = "courses" },
                    new Permission { Id = "PUBLISH_COURSES", Name = "نشر/إلغاء نشر الدورة", Description = "السماح بنشر أو إلغاء نشر الدورات", Module = "courses" },
                    new Permission { Id = "DELETE_COURSES", Name = "حذف الدورة", Description = "السماح بحذف الدورات", Module = "courses" },
                    new Permission { Id = "MANAGE_LESSONS", Name = "إدارة الدروس", Description = "السماح بإضافة وتعديل وحذف الدروس", Module = "courses" },
                    new Permission { Id = "MANAGE_VIDEOS", Name = "إدارة الفيديوهات", Description = "إدارة ورفع الفيديوهات الخاصة بالدروس", Module = "courses" },
                    new Permission { Id = "MANAGE_QUIZZES", Name = "إدارة الاختبارات", Description = "السماح بإنشاء وتعديل وحذف الاختبارات", Module = "courses" },
                    new Permission { Id = "GRADE_QUIZZES", Name = "تقييم الاختبارات", Description = "السماح بتقييم اختبارات الطلاب", Module = "courses" },
                    new Permission { Id = "MANAGE_MODULE_ITEMS", Name = "إدارة عناصر الوحدات", Description = "السماح بإضافة وتعديل وحذف عناصر الوحدات", Module = "courses" },

                    // Members & Roles
                    new Permission { Id = "VIEW_MEMBERS", Name = "عرض الأعضاء", Description = "السماح بعرض قائمة الأعضاء", Module = "members" },
                    new Permission { Id = "INVITE_MEMBERS", Name = "دعوة أعضاء", Description = "السماح بدعوة أعضاء جدد", Module = "members" },
                    new Permission { Id = "REMOVE_MEMBERS", Name = "إزالة الأعضاء", Description = "السماح بإزالة الأعضاء من المنظمة", Module = "members" },
                    new Permission { Id = "MANAGE_PERMISSIONS", Name = "إدارة الصلاحيات", Description = "السماح بتعديل صلاحيات الأدوار", Module = "members" },

                    // Website Builder
                    new Permission { Id = "EDIT_PAGES", Name = "تعديل صفحات الموقع", Description = "السماح بتعديل صفحات الموقع", Module = "website" },
                    new Permission { Id = "PUBLISH_SITE", Name = "نشر الموقع", Description = "السماح بنشر أو تحديث الموقع", Module = "website" },
                    new Permission { Id = "EDIT_HOMEPAGE", Name = "تعديل الصفحة الرئيسية", Description = "السماح بتعديل الصفحة الرئيسية", Module = "website" },
                    new Permission { Id = "MANAGE_CATALOG", Name = "إدارة الكاتالوج", Description = "السماح بإدارة كاتالوج الدورات", Module = "website" },
                    new Permission { Id = "MANAGE_SEO", Name = "إدارة SEO", Description = "السماح بتعديل إعدادات تحسين الظهور", Module = "website" },
                    new Permission { Id = "MANAGE_BRANDING", Name = "إدارة الهوية البصرية", Description = "السماح بإدارة الألوان والشعارات", Module = "website" },
                    new Permission { Id = "MANAGE_DOMAINS", Name = "إدارة النطاقات", Description = "السماح بربط وتعديل نطاقات الموقع", Module = "website" },

                    // Documents
                    new Permission { Id = "CREATE_DOCUMENTS", Name = "إنشاء مستندات", Description = "السماح بإنشاء مستندات جديدة", Module = "documents" },
                    new Permission { Id = "EDIT_DOCUMENTS", Name = "تعديل المستندات", Description = "السماح بتحرير المستندات", Module = "documents" },
                    new Permission { Id = "DELETE_DOCUMENTS", Name = "حذف المستندات", Description = "السماح بحذف المستندات", Module = "documents" },
                    new Permission { Id = "PUBLISH_DOCUMENTS", Name = "نشر المستندات", Description = "السماح بمشاركة أو نشر المستندات", Module = "documents" },

                    // Live Sessions
                    new Permission { Id = "CREATE_SESSIONS", Name = "إنشاء جلسات مباشرة", Description = "السماح بإنشاء الجلسات المباشرة", Module = "live-sessions" },
                    new Permission { Id = "EDIT_SESSIONS", Name = "تعديل الجلسات المباشرة", Description = "السماح بتعديل تفاصيل الجلسات", Module = "live-sessions" },
                    new Permission { Id = "START_END_SESSIONS", Name = "بدء/إنهاء الجلسات", Description = "السماح ببدء أو إنهاء الجلسات المباشرة", Module = "live-sessions" },
                    new Permission { Id = "VIEW_RECORDINGS", Name = "عرض التسجيلات", Description = "السماح بعرض تسجيلات الجلسات", Module = "live-sessions" },
                    new Permission { Id = "MANAGE_JOIN_PERMISSIONS", Name = "إدارة أذونات الانضمام", Description = "السماح بتحديد من يمكنه الانضمام", Module = "live-sessions" },
                    new Permission { Id = "INVITE_STUDENTS", Name = "دعوة الطلاب للجلسات", Description = "السماح بدعوة الطلاب إلى الجلسات المباشرة", Module = "live-sessions" },

                    // Payments
                    new Permission { Id = "VIEW_ORDERS", Name = "عرض الطلبات", Description = "السماح بعرض الطلبات", Module = "payments" },
                    new Permission { Id = "ISSUE_REFUNDS", Name = "إصدار المبالغ المستردة", Description = "السماح بإصدار واسترجاع المدفوعات", Module = "payments" },
                    new Permission { Id = "REVENUE_ANALYTICS", Name = "عرض تحليلات الإيرادات", Description = "السماح بعرض إحصائيات المبيعات", Module = "payments" },
                    new Permission { Id = "MANAGE_COUPONS", Name = "إدارة الكوبونات", Description = "السماح بإنشاء وتعديل وحذف الكوبونات", Module = "payments" },

                    // General
                    new Permission { Id = "VIEW_DASHBOARD", Name = "عرض لوحة التحكم", Description = "السماح بالوصول إلى لوحة التحكم", Module = "general" },
                    new Permission { Id = "VIEW_ANALYTICS", Name = "عرض التحليلات", Description = "السماح بعرض تحليلات الأداء", Module = "general" },
                    new Permission { Id = "MANAGE_NOTIFICATIONS", Name = "إدارة الإشعارات", Description = "السماح بإدارة إعدادات الإشعارات", Module = "general" },
                    new Permission { Id = "ACCESS_SETTINGS", Name = "الوصول إلى الإعدادات", Description = "السماح بالوصول إلى إعدادات النظام", Module = "general" },
                    new Permission { Id = "CHANGE_BRANDING", Name = "تغيير الهوية البصرية", Description = "السماح بتغيير الألوان والشعار", Module = "general" },
                    new Permission { Id = "MANAGE_INTEGRATIONS", Name = "إدارة التكاملات", Description = "السماح بإدارة ربط الخدمات الخارجية", Module = "general" },
                    new Permission { Id = "MANAGE_CALENDAR", Name = "إدارة التقويم", Description = "السماح بإدارة أحداث التقويم", Module = "general" },
                };
        }
    }
}