namespace BlazorDevIta.ERP.Infrastructure.Attributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class NotOrderableAttribute : Attribute
	{ }
}