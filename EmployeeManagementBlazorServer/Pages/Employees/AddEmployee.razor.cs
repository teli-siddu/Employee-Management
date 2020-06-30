using EmployeeManagementBlazorServer.Extensions;
using EmployeeManagementBlazorServer.Services;
using Entities.Helper;
using Entities.ViewModels;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Pages.Employees
{
    public partial class AddEmployee
    {

        [Inject]
        public IEmployeeService employeeService { get; set; }
        [Inject]
        public IDropdownsService dropdownsService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        AddEmployeeViewModel AddEmployeeView = new AddEmployeeViewModel();
        List<KeyValue<int, string>> Genders = new List<KeyValue<int, string>>();
        List<KeyValue<int, string>> MaritialStatuses = new List<KeyValue<int, string>>();
        List<KeyValue<int, string>> Nationalities = new List<KeyValue<int, string>>();
        List<KeyValue<int, string>> Countries = new List<KeyValue<int, string>>();
        List<KeyValue<int, string>> Departments = new List<KeyValue<int, string>>();
        List<RoleViewModel> roles = new List<RoleViewModel>();
        List<KeyValue<int, string>> States = new List<KeyValue<int, string>>();
        List<KeyValue<int, string>> Cities = new List<KeyValue<int, string>>();
        AddEmployeeDropdowns employeeDropdowns;
        
        MobileViewModel[] mobiles =
        {
            new MobileViewModel
            {
                MobileNumber="",
            },
            new MobileViewModel
            {
                MobileNumber="",
            }
        };
        EmailViewModel[] emails =
       {
            new EmailViewModel
            {
                EmailId="",
            },
            new EmailViewModel
            {
                EmailId="",
            }
        };

        AddAddressViewModel[] addAddresses =
        {
            new AddAddressViewModel()
        } ;

        //protected async override Task OnInitializedAsync()
        //{
        //    await _employeeService.AddEmployee()
        //}

        protected async override Task OnInitializedAsync()
        {
            try
            {
                roles = await employeeService.GetEmployeeRoles();
            }
            catch (Exception x) 
            {

            }
           
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }
        public override Task SetParametersAsync(ParameterView parameters)
        {
            return base.SetParametersAsync(parameters);
        }
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender && employeeDropdowns == null)
                {
                    employeeDropdowns = await employeeService.GetEmployeeDropdowns();


                    Departments = employeeDropdowns.Departments;
                    Genders = employeeDropdowns.Genders;
                    Nationalities = employeeDropdowns.Nationalities;
                    MaritialStatuses = employeeDropdowns.MaritialStatuses;
                    Countries = employeeDropdowns.Countries;
                    var uri = NavigationManager.Uri;
                    if (uri.ToLower().Trim().Contains("editemployee"))
                    {
                        var aUsri = NavigationManager.ToAbsoluteUri(uri);
                        NavigationManager.TryGetQueryString("Id", out int Id);
                        AddEmployeeView = await employeeService.GetEmployeeForEdit(Id);
                        emails = AddEmployeeView.Emails.ToArray();
                        mobiles = AddEmployeeView.Mobiles.ToArray();
                        addAddresses = AddEmployeeView.Addresses.ToArray();
                        States = await dropdownsService.States(addAddresses[0].CountryMasterId.Value);
                        Cities= await dropdownsService.Cities(addAddresses[0].StateMasterId.Value);
                        roles = AddEmployeeView.Roles;
                        // return;
                    }
                    StateHasChanged();
                }
            }
            catch (Exception x) 
            {
            }
          
           
        }

        protected async Task AddNewEmployee(EditContext context) 
        {
           AddEmployeeView.Mobiles =  mobiles.Where(x=>(x.MobileNumber!="")).ToList();
           AddEmployeeView.Emails = emails.Where(x => (x.EmailId != "")).ToList();
           AddEmployeeView.Addresses = addAddresses.ToList();
            AddEmployeeView.Roles = roles;
            var result= await employeeService.AddEmployee(AddEmployeeView);
            if (result.Succeeded) 
            {
                NavigationManager.NavigateTo("/Employees/Employees");
            }
        }

        protected async Task GetStates(int i,ChangeEventArgs eventArgs )
        {
          
            
            int countryId = Convert.ToInt32( eventArgs.Value);
            addAddresses[i].CountryMasterId = countryId;
            States = await dropdownsService.States(countryId);
        }
        protected async Task GetCities(int i,ChangeEventArgs eventArgs)
        {
            
            int stateId = Convert.ToInt32(eventArgs.Value);
            addAddresses[i].StateMasterId = stateId;
            Cities = await dropdownsService.Cities(stateId);
        }
        protected async Task EditEmployee(EditContext context)
        {
            if (!context.Validate()) 
            {
                return;
            }
            ReturnResult result= await employeeService.UpdateEmployee(AddEmployeeView);
            if (result.Succeeded) 
            {
                NavigationManager.NavigateTo("Employees/Employees");
            }
        }



    }
}
