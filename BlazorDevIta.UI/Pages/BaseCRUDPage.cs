using BlazorDevIta.ERP.Infrastructure.DataTypes;
using BlazorDevIta.UI.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorDevIta.UI.Pages
{
    public class BaseCRUDPage<ListItemType, DetailsType, IdType>
        : ComponentBase
        where ListItemType : BaseListItem<IdType>
        where DetailsType : BaseDetails<IdType>, new()
    {
        protected List<ListItemType?>? items = null;
        protected DetailsType? currentItem = null;

        [Inject]
        protected IDataServices<ListItemType, DetailsType, IdType>? DataServices { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (DataServices == null)
                throw new Exception("DataServices not provided");

            await RefreshData();
        }

        protected async Task RefreshData()
            => items = await DataServices!.GetAllAsync();

        protected void Create()
        {
            currentItem = new DetailsType();
        }

        protected async Task Edit(ListItemType item)
        {
            if (item.Id == null)
                throw new ArgumentException("item id cannot be null", "item.Id");

            currentItem = await DataServices!.GetByIdAsync(item.Id);
        }

        protected async Task Save(DetailsType item)
        {
            // if(item.Id != null && item.Id.Equals(default(IdType)))
            if(EqualityComparer<IdType>.Default.Equals(item.Id, default(IdType)))
            {
                await DataServices!.CreateAsync(item);
            }
            else
            {
                await DataServices!.UpdateAsync(item);
            }
            await RefreshData();
            currentItem = null;
        }


        protected async Task Delete(ListItemType item)
        {
            if (item.Id == null)
                throw new ArgumentException("item id cannot be null", "item.Id");

            await DataServices!.DeleteAsync(item.Id);
            await RefreshData();
        }

        protected void Cancel()
        {
            currentItem = null;
        }
    }
}
