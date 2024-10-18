using System;
using System.Collections.Generic;

namespace WebDevil.Model;

public partial class Devil
{
    public int Id { get; set; }

    /// <summary>
    /// погоняло
    /// </summary>
    public string Nick { get; set; }

    /// <summary>
    /// кол-во душ
    /// </summary>
    public int Rank { get; set; }

    public int Year { get; set; }

    public virtual ICollection<Rack> Racks { get; set; } = null;
}
