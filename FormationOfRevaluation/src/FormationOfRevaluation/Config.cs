using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;

namespace FormationOfRevaluation
{
    class Config
    {
        public static Procedures hCntMain { get; set; } //осн. коннект
        public static Procedures hCntAdd { get; set; } //доп. коннект
        public static DateTime CurDate { get; set; }
        public static string StatusCode { get; set; }
        public static string TTN { get; set; }
    }
}
