using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.Models
{
    public record CardBrandLimit(
        CardBrands CardBrand,
        decimal Amount);
}
