﻿using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class AggregatedCounter
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public long Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
