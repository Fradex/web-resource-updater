﻿using System.Runtime.Serialization;

namespace WebPackUpdater.Model.DTO
{
	[DataContract(Name = "buildrequestmodel")]
	public class BuildRequestModel
	{
		[DataMember(Name = "name")] public string Name { get; set; }
		[DataMember(Name = "description")] public string Description { get; set; }
	}
}