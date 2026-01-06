using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Application
{
    public record ChildrenPagedRequestDTO(int PageIndex = 1, int PageSize = 20);
    public record ChildrenFormFormDTO(ChildrenDTO Basic, ChildrenConditionDTO Condition, List<ChildrenImageDTO> ImageList);
    public record ChildrenConditionDTO(long? Id, Gender? Gender, EducationRequirement? MinEducation, int? MinAge, int? MaxAge, int? MinHeight, int? MaxHeight);
    public record ChildrenImageDTO(long? Id, string ImageUrl);
    public record ChildrenDTO(long? Id, string Name, string School, string BirthYear, MaritalStatus? MaritalStatus, string WhenMarried, Religion? Religion, string Nationality, string Hometown, string HometownIndex, string Address, string AddressIndex, string Occupation, string House, string VehicleInfo, BodyType? BodyType, Gender? Gender, Education? Education, IncomLevel IncomLevel, int? Height, string Remark);
}
