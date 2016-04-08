using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace SpanAidEmailSenderServie
{
    [RunInstaller(true)]
    public partial class SpanAidProjectInstaller : System.Configuration.Install.Installer
    {
        public SpanAidProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
