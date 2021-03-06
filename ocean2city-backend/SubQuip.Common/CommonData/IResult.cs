﻿using Ocean2City.Common.Enums;
using System;

namespace Ocean2City.Common.CommonData
{
    public interface IResult
    {
        Operation Operation { get; set; }

        Status Status { get; set; }

        String Message { get; set; }

        dynamic Body { get; set; }
    }

    public class Result : IResult
    {
        public Operation Operation { get; set; }
        public Status Status { get; set; }
        public String Message { get; set; }
        public dynamic Body { get; set; }
    }
}
