using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ToolsDev.LBTW.AuthSimulatorApp.Enums
{
    public enum TransMode
    {
        [Description("一般授權")]
        Authorization,

        [Description("分期")]
        Installment,

        [Description("紅利")]
        Redeem
    }
}