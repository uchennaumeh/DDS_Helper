﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDS_Tz_Helper.Business_Objects
{
    public class trade_dto
    {
        public string msg { get; set; }
        public string driver { get; set; }
        public string vehicle { get; set; }
        public string atc { get; set; }
        public bool statusPicking { get; set; }
        public string trip_ID { get; set; }
        public Boolean status { get; set; }
    }
}