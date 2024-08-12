using System;

namespace Domain.Base;

public interface IBaseEntity
{
	public int CreatedBy { get; }
	public DateTime CreatedOn { get; }
	public int? UpdatedBy { get; }
	public DateTime? ModifiedOn { get; }

	void SetAddedFields(int userId);

	void SetUpdatedFields(int userId);
}
