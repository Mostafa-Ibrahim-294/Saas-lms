using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Files.Dtos
{
    public sealed record ProcessingDto(EmbeddingDto Embedding, TranscriptDto Transcript, bool? ExtractText);
}
