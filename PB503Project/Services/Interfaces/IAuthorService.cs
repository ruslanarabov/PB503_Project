using System;
using PB503Project.DTOs.AuthorDTO;

namespace PB503Project.Services.Interfaces
{
	public interface IAuthorService
	{
        void Add(CreateAuthorDTO createAuthorDTO);

        void Remove(int Id);

        void Update(int Id, UpdateAuthorDTO updateAuthorDTO);

        List<GetAllAuthorsDTO> GetAll();
    }
}

