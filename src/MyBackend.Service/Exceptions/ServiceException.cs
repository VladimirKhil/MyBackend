﻿using MyBackend.Service.Contract.Models;
using System.Net;

namespace MyBackend.Service.Exceptions;

/// <summary>
/// Defines a common service exception.
/// </summary>
public sealed class ServiceException : Exception
{
    /// <summary>
    /// Error code.
    /// </summary>
    public WellKnownMyBackendServiceErrorCode ErrorCode { get; }

    /// <summary>
    /// HTTP error status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Exception parameters.
    /// </summary>
    public Dictionary<string, object>? Parameters { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="ServiceException" /> class.
    /// </summary>
    /// <param name="errorCode">Error code.</param>
    /// <param name="statusCode">HTTP error status code.</param>
    /// <param name="parameters">Exception parameters.</param>
    public ServiceException(
        WellKnownMyBackendServiceErrorCode errorCode,
        HttpStatusCode statusCode,
        Dictionary<string, object>? parameters = null)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
        Parameters = parameters;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ServiceException" /> class.
    /// </summary>
    /// <param name="errorCode">Error code.</param>
    /// <param name="statusCode">HTTP error status code.</param>
    /// <param name="innerException">Inner exception that caused current.</param>
    public ServiceException(WellKnownMyBackendServiceErrorCode errorCode, HttpStatusCode statusCode, Exception? innerException)
        : base(null, innerException)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}
