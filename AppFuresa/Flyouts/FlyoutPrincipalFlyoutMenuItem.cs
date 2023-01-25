using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFuresa.Flyouts
{
    public class FlyoutPrincipalFlyoutMenuItem
    {
        public FlyoutPrincipalFlyoutMenuItem()
        {
            TargetType = typeof(FlyoutPrincipalFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}