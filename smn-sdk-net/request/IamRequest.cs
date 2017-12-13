﻿/*
 * Copyright (C) 2017. Huawei Technologies Co., LTD. All rights reserved.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of Apache License, Version 2.0.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * Apache License, Version 2.0 for more details.
 */
using System.Net;
using System.Runtime.Serialization;
using Smn.Http;
using Smn.Response;
using Smn.Util;

namespace Smn.Request
{
    ///<summary> 
    /// iam auth request message
    /// author:zhangyx
    /// version:1.0.0
    ///</summary> 
    [DataContract]
    class IamRequest : AbstractRequest<IamRepsonse>
    {
        public override HttpMethod GetHttpMethod()
        {
            return HttpMethod.POST;
        }

        public override string GetBodyParams()
        {
            return GetRequestMessage();
        }

        public override int? GetTimeout()
        {
            return null;
        }

        public override string GetUrl()
        {
            return Constants.HTTPS + Constants.IAM + "." + SmnConfiguration.RegionName + "." + Constants.ENDPOINT + Constants.IAM_TOKEN_URI;
        }

        private string GetRequestMessage()
        {
            return "{" +
                "\"auth\": {" +
                    "\"identity\": {" +
                        "\"methods\": [" +
                            "\"password\"" +
                    "]," +
                    "\"password\": {" +
                        "\"user\": {" +
                            "\"name\": \"" + this.SmnConfiguration.Username + "\"," +

                            "\"password\":\"" + this.SmnConfiguration.Password + "\"," +
                            "\"domain\": {" +
                                "\"name\": \"" + this.SmnConfiguration.DomainName + "\"" +
                            "}" +
                        "}" +
                    "}" +
                "}," +
                "\"scope\": {" +
                    "\"project\": {" +
                        "\"name\":\"" + this.SmnConfiguration.RegionName + "\"" +
                    "}" +
                "}" +
            "}" +
        "}";
        }
    }
}