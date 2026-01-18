using PrettyLove.Domain.Shared;

namespace PrettyLove.Application
{
    public record ChildrenPagedRequestDTO(Gender? Gender, int? MinAge, int? MaxAge, int? MinHeight, int? MaxHeight, int PageIndex = 1, int PageSize = 20);
    public record ChildrenFormFormDTO(ChildrenDTO Basic, ChildrenConditionDTO Condition, List<ChildrenImageDTO> ImageList);
    public record ChildrenConditionDTO(long? Id, Gender? Gender, EducationRequirement? MinEducation, int? MinAge, int? MaxAge, int? MinHeight, int? MaxHeight);
    public record ChildrenImageDTO(long? Id, string Url);
    public record ChildrenDTO(long? Id, string Name, string School, DateTime? BirthYear, MaritalStatus? MaritalStatus, string WhenMarried, Religion? Religion, string Nationality, string Hometown, string HometownIndex, string Address, string AddressIndex, string Occupation, string House, string VehicleInfo, BodyType? BodyType, Gender? Gender, Education? Education, IncomLevel IncomLevel, int? Height, string Remark);
}
