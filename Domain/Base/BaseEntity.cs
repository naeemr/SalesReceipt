using System;

namespace Domain.Base;

public abstract class BaseEntity : IBaseEntity
{
	public int Id { get; private set; }
	public int CreatedBy { get; private set; }
	public DateTime CreatedOn { get; private set; }
	public int? UpdatedBy { get; private set; }
	public DateTime? ModifiedOn { get; private set; }

	public BaseEntity() { }

	public BaseEntity(int id) //This will be auto generated in database, but I am keeping as contructor parameter to create in-memory data
	{
		Id = id;
	}


	public void SetAddedFields(int userId)
	{
		CreatedOn = DateTime.UtcNow;
		CreatedBy = userId;
	}

	public void SetUpdatedFields(int userId)
	{
		ModifiedOn = DateTime.UtcNow;
		UpdatedBy = userId;
	}
}
