﻿namespace BikesBooking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.DTO.ContactModels;
    using Microsoft.EntityFrameworkCore;

    public class ContactService : IContactService
    {
        private readonly IDeletableEntityRepository<Contact> contactRepository;

        public ContactService(IDeletableEntityRepository<Contact> contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<IEnumerable<ContactFormDto>> GetAllMessages()
            => await this.contactRepository.AllAsNoTracking()
            .AsQueryable()
            .Select(x => new ContactFormDto
            {
                Id = x.Id,
                Name = x.Username,
                Email = x.Email,
                Subject = x.Complaint,
                Description = x.Description,
            })
            .ToListAsync();

        public async Task SendContactMessageAsync(ContactFormDto contact)
        {
            var message = new Contact
            {
                Username = contact.Name,
                Email = contact.Email,
                Complaint = contact.Subject,
                Description = contact.Description,
            };

            await this.contactRepository.AddAsync(message);
            await this.contactRepository.SaveChangesAsync();
        }
    }
}
