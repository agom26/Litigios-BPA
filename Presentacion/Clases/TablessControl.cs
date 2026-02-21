using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Clases
{
    public class TablessControl : TabControl
    {
        protected override void WndProc(ref Message m)
        {
            // WM_NCPAINT = 0x85
            if (m.Msg == 0x1328) // TCM_ADJUSTRECT
            {
                if (!DesignMode)
                {
                    m.Result = (IntPtr)1; // Ignora la parte de las pestañas
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
