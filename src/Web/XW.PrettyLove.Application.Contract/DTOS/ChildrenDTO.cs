using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Application
{
    public record ChildrenPagedRequestDTO(int PageIndex = 1, int PageSize = 20);
    public record ChildrenFormFormDTO(ChildrenDTO Basic, ChildrenConditionDTO Condition, List<ChildrenImageDTO> ImageList);
    public record ChildrenConditionDTO(long? Id, Gender? Gender, Education? MinEducation, Education? MaxEducation, int? MinAge, int? MaxAge, int? MinHeight, int? MaxHeight);
    public record ChildrenImageDTO(long? Id, string ImageUrl);
    public record ChildrenDTO(long? Id, string Name, MaritalStatus? MaritalStatus, string Nationality, string Hometown, string Address, string House, string VehicleInfo, BodyType? BodyType, Gender? Gender, Education? Education, IncomLevel IncomLevel, int? Height, string Remark);
}
