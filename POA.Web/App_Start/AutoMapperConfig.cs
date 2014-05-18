using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using POA.Web.Infrastructure.Mapping;
using POA.Web.Infrastructure.Tasks;
using POA.Web.Models.Title;
using POA.Web.Models.Agent;
using System.Data.Entity.SqlServer;
using POA.Web.Models.Category;
using POA.Web.Models.SubCategory;
using POA.Web.Models.Country;
using POA.Web.Models.Template;
using POA.Web.Models.PowerOfAttorney;

namespace POA.Web.App_Start
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
                     
            MapAgents();
            MapCategories();
            MapCountries();
            MapSubCategories();
            MapTemplates();
            MapTitles();
        }

             

        private static void MapAgents()
        {
            Mapper.CreateMap<Domain.Agent, EditAgentForm>()
                .ForMember(m=>m.Title_ID, opt=>opt.MapFrom(i=>SqlFunctions.StringConvert((double)i.Title_ID).Trim()))
                .ForMember(m => m.Country_Of_Birth_ID, opt => opt.MapFrom(i => SqlFunctions.StringConvert((double)i.Country_Of_Birth_ID).Trim()))
                .ForMember(m => m.Country_Of_CitizenShip_ID, opt => opt.MapFrom(i => SqlFunctions.StringConvert((double)i.Country_Of_CitizenShip_ID).Trim()))
                .ForMember(m => m.Country_Of_Residence_ID, opt => opt.MapFrom(i => SqlFunctions.StringConvert((double)i.Country_Of_Residence_ID).Trim()));
            Mapper.CreateMap<Domain.Agent, NewAgentForm>();
            Mapper.CreateMap<Domain.Agent, AgentSummaryViewModel>();
            Mapper.CreateMap<Domain.Agent, AgentDetailsViewModel>();
            Mapper.CreateMap<Domain.Agent, AgentModel>();
        }

        private static void MapCategories()
        {


            Mapper.CreateMap<Domain.Category, EditCategoryForm>()
                 .ForMember(m => m.SubCategories, opt => opt.MapFrom(i => SqlFunctions.StringConvert((double)i.Category_ID).Trim()));          
            Mapper.CreateMap<Domain.Category, NewCategoryForm>();                
            Mapper.CreateMap<Domain.Category, CategorySummaryViewModel>();
            Mapper.CreateMap<Domain.Category, CategoryDetailsViewModel>();
        }

        private static void MapCountries()
        {
            Mapper.CreateMap<Domain.Country, EditCountryForm>();
            Mapper.CreateMap<Domain.Country, NewCountryForm>();
            Mapper.CreateMap<Domain.Country, CountrySummaryViewModel>();
            Mapper.CreateMap<Domain.Country, CountryDetailsViewModel>();
        }

        private static void MapSubCategories()
        {
            Mapper.CreateMap<Domain.SubCategory, EditSubCategoryForm>()
                .ForMember(m => m.Category, opt => opt.MapFrom(i => SqlFunctions.StringConvert((double)i.SubCategory_ID).Trim()));
            Mapper.CreateMap<Domain.SubCategory, NewSubCategoryForm>();
            Mapper.CreateMap<Domain.SubCategory, SubCategorySummaryViewModel>();
            Mapper.CreateMap<Domain.SubCategory, SubCategoryDetailsViewModel>();
        }

        private static void MapTemplates()
        {
            Mapper.CreateMap<Domain.Template, EditTemplateForm>();
            Mapper.CreateMap<Domain.Template, NewTemplateForm>();
            Mapper.CreateMap<Domain.Template, TemplateSummaryViewModel>();
            Mapper.CreateMap<Domain.Template, TemplateDetailsViewModel>();
        }

        private static void MapTitles()
        {
            Mapper.CreateMap<Domain.Title, EditTitleForm>();
            Mapper.CreateMap<Domain.Title, NewTitleForm>();
            Mapper.CreateMap<Domain.Title, TitleSummaryViewModel>();
            Mapper.CreateMap<Domain.Title, TitleDetailsViewModel>();
        }
    }
}