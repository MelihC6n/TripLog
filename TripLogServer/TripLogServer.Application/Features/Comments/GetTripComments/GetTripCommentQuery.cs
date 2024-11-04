﻿using MediatR;
using TripLogServer.Application.Features.Trips.GetAll;
using TS.Result;

namespace TripLogServer.Application.Features.Comments.GetTripComments;
public sealed record GetTripCommentQuery(
    Guid TripId) : IRequest<Result<List<QueryComment>>>;
