using System;
using HikeService.HikesModule.Models;

namespace HikeService.HikesModule.Services
{
	public interface HikeDetailsService
	{
		HikeDetails GetHikeInformation(String url);
        TripDetails GetTripInformation(String url);
	}
}
