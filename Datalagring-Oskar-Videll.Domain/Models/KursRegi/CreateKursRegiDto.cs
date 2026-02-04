using System;
using System.Collections.Generic;
using System.Text;

namespace DatalagringOskarVidell.Domain.Models.KursRegi;

public sealed record CreateKursRegiDto(
    DateTime RegistrationDate,
    string Status
);