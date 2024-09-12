using EasyLectureModel.Dto;
using EasyLectureModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Interface
{
    public interface IRole
    {
        List<RoleDto> GetList();
        RoleDto Get(int id);
    }
}
