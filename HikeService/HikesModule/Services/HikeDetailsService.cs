using System;
using CommonModels.Hike;

namespace HikeService.HikesModule.Services
{
	public interface IHikeDetailsService
	{
		HikeDetails GetHikeInformation(String url);
        TripDetails GetTripInformation(String url);
	}
}
