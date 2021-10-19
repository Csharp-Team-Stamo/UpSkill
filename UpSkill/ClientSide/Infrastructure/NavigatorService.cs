namespace UpSkill.ClientSide.Infrastructure
{
    using Microsoft.AspNetCore.Components;

    public class NavigatorService : INavigatorService
    {
        private readonly NavigationManager navigationManager;

        public NavigatorService(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }

        public void RedirectToPage(string pagePath)
        {
            navigationManager.NavigateTo(pagePath);
        }
    }
}
