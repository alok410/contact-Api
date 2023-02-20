using ContactsApi.Data;
using ContactsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContactsApi.Controllers
{

/*
    to add database*/
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsApiDbContext dbContext;
        public ContactsController(ContactsApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ContactsApiDbContext DbContext { get; }



      /*  to get all results*/
        [HttpGet]
       
        public async Task<IActionResult> GetContacts()
        {
            
            return Ok(await dbContext.Contacts.ToListAsync()) ;
        }

/*
        to get single result using id*/
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSingleContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }



        /*to add new data*/
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email, 
                Name = addContactRequest.Name,
                Phone = addContactRequest.Phone,

            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }



      /*  to update/edit data*/
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContactRequest updatecontactrequest)
        {

            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact !=  null )
            {
                contact.Address = updatecontactrequest.Address;
                contact.Email = updatecontactrequest.Email;
                contact.Name = updatecontactrequest.Name;
                contact.Phone = updatecontactrequest.Phone;


               await dbContext.SaveChangesAsync();

                return Ok(contact); 
            }
            return NotFound();

        }


/*
        to delete data*/
        [HttpDelete]
        [Route("{id}:guid")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
             if(contact != null)
             { 
                dbContext.Remove(contact);
               await dbContext.SaveChangesAsync(); 
             }
            return NotFound();
        }


    }
}
