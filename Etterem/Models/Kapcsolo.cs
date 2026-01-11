using System;
using System.Collections.Generic;

namespace Etterem.Models;

public partial class Kapcsolo
{
    public int Kapcsoloid { get; set; }

    public int? Rendelesid { get; set; }

    public int? Termekekid { get; set; }
}
