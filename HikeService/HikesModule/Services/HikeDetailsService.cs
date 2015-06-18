using System;
using HikeService.HikesModule.Models;

namespace HikeService.HikesModule.Services
{
	public interface HikeDetailsService
	{
		HikeDetails GetInformation(String url);
	}
}
