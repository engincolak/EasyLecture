using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Utils
{
    public class DbParam : DbParameter
    {
        public DbParam(string parameterName, object value)
        {
            this.ParameterName = parameterName;
            this.Value = value;
        }

        public DbParam(string parameterName, object value, DbType dbType)
        {
            this.ParameterName = parameterName;
            this.Value = value;
            this.DbType = dbType;
        }

        public override object Value
        {
            get;
            set;
        }

        public override string ParameterName
        {
            get;
            set;
        }

        public override DbType DbType
        {
            get;

            set;
        }

        public override ParameterDirection Direction
        {
            get;

            set;
        }

        public override bool IsNullable
        {
            get;

            set;
        }

        public override int Size
        {
            get;

            set;
        }

        public override string SourceColumn
        {
            get;

            set;
        }

        public override bool SourceColumnNullMapping
        {
            get;

            set;
        }

        public override void ResetDbType()
        {
            throw new NotImplementedException();
        }
    }
}
