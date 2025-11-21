namespace HUP.Core.Entities.Academics;

public class CourseOfferingInstructor
{
    public Guid CourseOfferingId { get; set; }
    public Guid InstructorId { get; set; }
    
    public Instructor Instructor { get; set; }
    public CourseOffering CourseOffering { get; set; }
}