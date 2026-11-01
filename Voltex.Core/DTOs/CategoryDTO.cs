using System;
using System.Collections.Generic;
using System.Text;

namespace Voltex.Core.DTOs
{
    public record CategoryDTO(string CategoryTitle, string Description);
    public record UpdateCategoryDTO(int id, string Titel, string Description);
}
