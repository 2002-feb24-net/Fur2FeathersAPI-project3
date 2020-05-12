using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Furs2Feathers.DataAccess
{
    public class Mapper
    {
        ///-------------------------------------------------------------------------------------------------------------------
        ///-------------------------------------------------------------------------------------------------------------------
        ///-------------------------------------------------------------------------------------------------------------------
        ///-------------------------------------------------------------------------------------------------------------------
        ///-------------------------------------------------------------------------------------------------------------------
        public static Domain.Models.Address MapAddress(Models.Address address)
        {
            return new Domain.Models.Address
            {
                AddressId = address.AddressId,
                Street = address.Street,
                City = address.City,
                State = address.State,
                Zip = address.Zip
            };
        }

        public static Domain.Models.Claims MapClaims(Models.Claims claims)
        {
            return new Domain.Models.Claims
            {
                ClaimId = claims.ClaimId,
                PolicyId = claims.PolicyId,
                Description = claims.Description,
                FilingDate = claims.FilingDate
            };
        }

        public static Domain.Models.Customer MapCustomer(Models.Customer customer)
        {
            return new Domain.Models.Customer
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Policies = customer.Policies,
                street = customer.AddressNavigation?.Street,
                state = customer.AddressNavigation?.State,
                city = customer.AddressNavigation?.City,
                zip = customer.AddressNavigation?.Zip
            };
        }

        public static Domain.Models.Employee MapEmployee(Models.Employee employee)
        {
            return new Domain.Models.Employee
            {
                EmpId = employee.EmpId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Invoice = employee.Invoice.Select(MapInvoice).ToList(),
                Policies = employee.Policies.Select(MapPolicies).ToList()
            };
        }

        public static Domain.Models.Invoice MapInvoice(Models.Invoice invoice)
        {
            return new Domain.Models.Invoice
            {
                InvoiceId = invoice.InvoiceId,
                CustomerId = invoice.CustomerId,
                EmpId = invoice.EmpId,
                PetId = invoice.PetId,
                Notes = invoice.Notes,
                Cost = invoice.Cost
            };
        }

        public static Domain.Models.Pet MapPet(Models.Pet pet)
        {
            return new Domain.Models.Pet
            {
                PetId = pet.PetId,
                Species = pet.Species,
                Name = pet.Name,
                Description = pet.Description,
                ImgUrl = pet.ImgUrl,
                Age = pet.Age,
                Sex = pet.Sex,
                Invoice = pet.Invoice.Select(MapInvoice).ToList(),
                Policies = pet.Policies.Select(MapPolicies).ToList()
            };
        }

        public static Domain.Models.Plan MapPlan(Models.Plan plan)
        {
            return new Domain.Models.Plan
            {
                PlanId = plan.PlanId,
                Description = plan.Description,
                EstCost = plan.EstCost,
                PositivesMax = plan.PositivesMax,
                PlanProLabels = plan.PlanProLabels.Select(MapPlanProLabels).ToList()
            };
        }

        public static Domain.Models.PlanProLabels MapPlanProLabels(Models.PlanProLabels planProLabels)
        {
            return new Domain.Models.PlanProLabels
            {
                PlanId = planProLabels.PlanId,
                PlanProLabelsId = planProLabels.PlanProLabelsId,
                Labels = planProLabels.Labels
            };
        }

        public static Domain.Models.PlanReviews MapPlanReviews(Models.PlanReviews planReviews)
        {
            return new Domain.Models.PlanReviews
            {
                PlanReviewId = planReviews.PlanReviewId,
                CustomerId = planReviews.CustomerId,
                Review = planReviews.Review
            };
        }

        public static Domain.Models.Policies MapPolicies(Models.Policies policies)
        {
            return new Domain.Models.Policies
            {
                PolicyId = policies.PolicyId,
                EmpId = policies.EmpId,
                Active = policies.Active,
                Deductible = policies.Deductible,
                PetId = policies.PetId,
                Premium = policies.Premium,
                RenewalDate = policies.RenewalDate,
                Claims = policies.Claims.Select(MapClaims).ToList(),
            };
        }
        public static Models.Address MapAddress(Domain.Models.Address address)
        {
            return new Models.Address
            {
                Street = address.Street,
                City = address.City,
                State = address.State,
                Zip = address.Zip
            };
        }
        ///-------------------------------------------------------------------------------------------------------------------
        ///-------------------------------------------------------------------------------------------------------------------
        ///-------------------------------------------------------------------------------------------------------------------
        ///-------------------------------------------------------------------------------------------------------------------
        ///-------------------------------------------------------------------------------------------------------------------
        public static Models.Address MapAddress(Domain.Models.Customer address)
        {
            return new Models.Address
            {
                Street = address.street,
                City = address.city,
                State = address.state,
                Zip = address.zip
            };
        }

        public static Models.Claims MapClaims(Domain.Models.Claims claims)
        {
            return new Models.Claims
            {
                ClaimId = claims.ClaimId,
                PolicyId = claims.PolicyId,
                Description = claims.Description,
                FilingDate = claims.FilingDate
            };
        }

        public static Models.Customer MapCustomer(Domain.Models.Customer customer)
        {
            return new Models.Customer
            {
                CustomerId = customer.CustomerId,
                Email = customer.Email,
                Phone = customer.Phone,
                Policies = customer.Policies,
                //AddressNavigation = MapAddress(customer.AddressNavigation)
                //PoliciesNavigation = MapPolicies(customer.PoliciesNavigation),
                //Invoice = customer.Invoice.Select(MapInvoice).ToList(),
                //PlanReviews = customer.PlanReviews.Select(MapPlanReviews).ToList()

            };
        }

        public static Models.Employee MapEmployee(Domain.Models.Employee employee)
        {
            return new Models.Employee
            {
                EmpId = employee.EmpId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Invoice = employee.Invoice.Select(MapInvoice).ToList(),
                Policies = employee.Policies.Select(MapPolicies).ToList()
            };
        }

        public static Models.Invoice MapInvoice(Domain.Models.Invoice invoice)
        {
            return new Models.Invoice
            {
                InvoiceId = invoice.InvoiceId,
                CustomerId = invoice.CustomerId,
                EmpId = invoice.EmpId,
                PetId = invoice.PetId,
                Notes = invoice.Notes,
                Cost = invoice.Cost
            };
        }

        public static Models.Pet MapPet(Domain.Models.Pet pet)
        {
            return new Models.Pet
            {
                PetId = pet.PetId,
                Species = pet.Species,
                Name = pet.Name,
                Description = pet.Description,
                ImgUrl = pet.ImgUrl,
                Age = pet.Age,
                Sex = pet.Sex,
                Invoice = pet.Invoice.Select(MapInvoice).ToList(),
                Policies = pet.Policies.Select(MapPolicies).ToList()
            };
        }

        public static Models.Plan MapPlan(Domain.Models.Plan plan)
        {
            return new Models.Plan
            {
                PlanId = plan.PlanId,
                Description = plan.Description,
                EstCost = plan.EstCost,
                PositivesMax = plan.PositivesMax,
                PlanProLabels = plan.PlanProLabels.Select(MapPlanProLabels).ToList()
            };
        }

        public static Models.PlanProLabels MapPlanProLabels(Domain.Models.PlanProLabels planProLabels)
        {
            return new Models.PlanProLabels
            {
                PlanId = planProLabels.PlanId,
                PlanProLabelsId = planProLabels.PlanProLabelsId,
                Labels = planProLabels.Labels
            };
        }

        public static Models.PlanReviews MapPlanReviews(Domain.Models.PlanReviews planReviews)
        {
            return new Models.PlanReviews
            {
                PlanReviewId = planReviews.PlanReviewId,
                CustomerId = planReviews.CustomerId,
                Review = planReviews.Review
            };
        }

        public static Models.Policies MapPolicies(Domain.Models.Policies policies)
        {
            return new Models.Policies
            {
                PolicyId = policies.PolicyId,
                EmpId = policies.EmpId,
                Active = policies.Active,
                Deductible = policies.Deductible,
                PetId = policies.PetId,
                Premium = policies.Premium,
                RenewalDate = policies.RenewalDate,
                Claims = policies.Claims.Select(MapClaims).ToList()
            };
        }
    }
}
