using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        //to discuss (q: course might be available for many departments at the same faculty => University Requirements & Faculty Requirements)
    }
}
