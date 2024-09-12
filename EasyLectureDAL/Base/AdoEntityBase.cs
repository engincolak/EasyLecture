using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLectureDAL.Provider;
using EasyLectureDAL.Repository.Ado.Sql;

namespace EasyLectureDAL.Base
{
    public class AdoEntityBase
    {
        private AdoProvider adoProvider;

        protected AdoProvider Provider { get; init; }
        protected ScriptManager Script { get; init; }

        public AdoEntityBase(AdoProvider provider, ScriptManager script)
        {
            Provider = provider;
            Script = script;
        }

        public AdoEntityBase(AdoProvider adoProvider)
        {
            this.adoProvider = adoProvider;
        }
    }
}
