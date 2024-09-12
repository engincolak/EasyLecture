using EasyLectureDAL.Base;
using EasyLectureDAL.Repository.Interface;
using EasyLectureModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.EF.Entity
{
    internal class RoleEF : EFEntityBase, IRole
    {
        public RoleDto Get(int id)
        {
            var result = Role.FirstOrDefault(x => x.RolID == id);

            if (result != null && result.RolIsActive)
            {
                var result2 = new RoleDto
                {
                    RolID = result.RolID,
                    RolName = result.RolName
                };
                return result2;
            }

            return null;
        }


        public List<RoleDto> GetList()
        {
            return Role
                .Where(u => u.RolIsActive) // Gerekirse bu filtreyi kontrol edin
                .Select(u => new RoleDto
                {
                    RolID = u.RolID,
                    RolName = u.RolName,
                    RolIsActive = u.RolIsActive
                })
                .ToList();

        }
    }
}
