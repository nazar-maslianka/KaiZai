using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KaiZai.Common.Types;

namespace KaiZai.Services.Incomes.BAL.DTOs;

public sealed record AddUpdateIncomeDTO(Guid CategoryId, [Required] DateTimeOffset IncomeDate, string Description, decimal Amount);