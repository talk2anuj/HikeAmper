using System;
using CommonModels.Hike;

namespace HikeService.HikesModule.Services
{
	public interface HikeDetailsService
	{
		HikeDetails GetHikeInformation(String url);
        TripDetails GetTripInformation(String url);
	}
}
