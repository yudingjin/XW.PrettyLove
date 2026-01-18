namespace PrettyLove.Application
{
    public record SmsRequestDTO(string Mobile, string Code);
    public record SmsResponseDTO(string Status, string Code, string Msg);
}
