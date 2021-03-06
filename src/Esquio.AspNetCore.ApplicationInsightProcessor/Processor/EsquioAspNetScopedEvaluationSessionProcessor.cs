﻿using Esquio.Abstractions;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Esquio.AspNetCore.ApplicationInsightProcessor.Processor
{
    public sealed class EsquioAspNetScopedEvaluationSessionProcessor
        : ITelemetryProcessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITelemetryProcessor _next;

        public EsquioAspNetScopedEvaluationSessionProcessor(ITelemetryProcessor next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public void Process(ITelemetry item)
        {
            AddEsquioProperties(item);

            if (_next != null)
            {
                _next.Process(item);
            }
        }

        private void AddEsquioProperties(ITelemetry telemetryItem)
        {
            if (_httpContextAccessor.HttpContext != null && telemetryItem != null)
            {
                var evaluation = _httpContextAccessor.HttpContext?
                    .Items
                    .Where(k => k.Key.ToString() == EsquioConstants.ESQUIO)
                    .Select(i => i.Value)
                    .SingleOrDefault();

                if (evaluation != null)
                {
                    var items = evaluation as Dictionary<string, bool>;

                    if (items != null)
                    {
                        ISupportProperties telemetry = telemetryItem as ISupportProperties;

                        foreach (var (key, value) in items)
                        {
                            if (!telemetry.Properties.ContainsKey(key.ToString()))
                            {
                                telemetry.Properties.Add(key.ToString(), value.ToString());
                            }
                        }
                    }
                }
            }
        }
    }
}
