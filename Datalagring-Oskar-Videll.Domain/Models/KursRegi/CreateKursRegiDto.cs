using System;
using System.Collections.Generic;
using System.Text;

namespace Datalagring_Oskar_Videll.Domain.Models.KursRegi;

public sealed record CreateKursRegiDto(
    DateTime RegistrationDate,
    string Status
);