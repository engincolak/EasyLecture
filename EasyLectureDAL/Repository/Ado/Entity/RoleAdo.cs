using EasyLectureDAL.Base;
using EasyLectureDAL.Provider;
using EasyLectureDAL.Repository.Ado.Sql;
using EasyLectureDAL.Repository.Interface;
using EasyLectureDAL.Utils;
using EasyLectureModel.Dto;
using EasyLectureModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Ado.Entity
{
    public class RoleAdo : AdoEntityBase, IRole
    {
        public RoleAdo(AdoProvider provider, ScriptManager script) : base(provider, script)
        {
        }

        public List<RoleDto> GetList()
        {
            List<RoleDto> resultList = new List<RoleDto>();

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                resultList.Add(new RoleDto()
                {
                    RolID = reader.GetInt32(0),
                    RolName = reader.GetString(1),
                    
                });
            };

            Provider.GetList(funcs, Script.Role.GetList);

            return resultList;
        }

        public RoleDto Get(int id)
        {
            RoleDto result = null;

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                result = new RoleDto()
                {
                    RolID = reader.GetInt32(0),
                    RolName = reader.GetString(1),
                };
            };

            List<DbParam> dbParams = new()
            {
                new DbParam("@UstId", id)
            };

            Provider.GetSingle(funcs, Script.Role.Get, dbParams);

            return result;
        }
    }
    }
