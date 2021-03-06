﻿using Smn;
using Smn.Request.Sms;
using Smn.Response.Sms;
using System;
using System.Collections.Generic;

namespace Smn.Example
{
    class SmsDemo
    {
        private SmnClient smnClient;

        public SmsDemo(SmnClient smnClient)
        {
            this.smnClient = smnClient;
        }

        /// <summary>
        /// 发送短信通知验证码
        /// </summary>
        public void SmsPublish()
        {
            // 设置请求对象
            SmsPublishRequest request = new SmsPublishRequest
            {
                // MessageIncludeSignFlag默认为false，SignId必填，
                // 为true时，可以不传SignId, 但是内容中必须包含签名,如【华为云】您的验证码是:1234，请查收。签名以【】括起来放在内容头部或者尾部
                MessageIncludeSignFlag = false,
                // 短信签名.需要在消息通知服务的自助页面申请签名，申请办理时间约2天
                SignId = "6be340e91e5241e4b5d85837e6709104",
                // 发送手机号码 号码格式 (+)(国家码)(手机号码)
                Endpoint = "86136*****87",
                Message = "【华为企业云】您的验证码是:12345，请查收"
            };
            try
            {
                // 发送请求并返回响应
                SmsPublishResponse response = smnClient.SendRequest(request);
                string result = response.MessageId;
                Console.WriteLine("{0}， {1}", result, response.ErrMessage);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 批量发送通知类验证码类短信demo
        /// </summary>
        public void SmsBatchPublish()
        {
            // 发送手机号码 号码格式 (+)(国家码)(手机号码)
            List<string> endpoints = new List<string>
            {
                "86136*****87"
            };

            // 设置请求对象
            SmsBatchPublishRequest request = new SmsBatchPublishRequest
            {
                // MessageIncludeSignFlag默认为false，SignId必填，
                // 为true时，可以不传SignId, 但是内容中必须包含签名,如【华为云】您的验证码是:1234，请查收。签名以【】括起来放在内容头部或者尾部
                MessageIncludeSignFlag = false,
                // 短信签名,需要在消息通知服务的自助页面申请签名，申请办理时间约2天
                SignId = "6be340e91e5241e4b5d85837e6709104",
                Endpoints = endpoints,
                Message = "你好，您的验证码是:12345679，请查收"
            };
            try
            {
                // 发送请求并返回响应
                SmsBatchPublishResponse response = smnClient.SendRequest(request);
                if (response.IsSuccess())
                {
                    List<SmsBatchPublishResult> results = response.Result;
                    foreach (SmsBatchPublishResult result in results)
                    {
                        Console.WriteLine("messageId={0}, endpoint={1}, code={2}, message={3}",
                            result.MessageId, result.Endpoint, result.Code, result.Message);
                    }
                }
                else
                {
                    Console.WriteLine("code={0}, message={1}, statusCode={2}",
                        response.ErrCode, response.ErrMessage, response.StatusCode);
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
                Console.ReadLine();
            }
        }

        public void SmsBatchPublishWithDiffMessage()
        {
            SmsPublishMessage smsPublishMessage1 = new SmsPublishMessage()
            {
                MessageIncludeSignFlag = false,
                // 短信签名,需要在消息通知服务的自助页面申请签名，申请办理时间约2天
                SignId = "3fe9fae14729495990cf1a3218fe2aca",
                Endpoint = "86136*****87",
                Message = "你好，您的验证码是:12345679，请查收"
            };

            SmsPublishMessage smsPublishMessage2 = new SmsPublishMessage()
            {
                // MessageIncludeSignFlag默认为false，SignId必填，
                // 为true时，可以不传SignId, 但是内容中必须包含签名,如【华为云】您的验证码是:1234，请查收。签名以【】括起来放在内容头部或者尾部
                MessageIncludeSignFlag = true,
                // 短信签名,需要在消息通知服务的自助页面申请签名，申请办理时间约2天
                Endpoint = "86186****875",
                Message = "【华为云华南测试】你好，您的验证码是:1234567，请查收"
            };

            List<SmsPublishMessage> smsMessages = new List<SmsPublishMessage>
            {
                smsPublishMessage1,
                smsPublishMessage2
            };
            SmsBatchPublishWithDiffMessageRequest request = new SmsBatchPublishWithDiffMessageRequest()
            {
                SmsMessages = smsMessages
            };

            try
            {
                // 发送请求并返回响应
                SmsBatchPublishWithDiffMessageResponse response = smnClient.SendRequest(request);
                if (response.IsSuccess())
                {
                    List<SmsBatchPublishWithDiffMessageResult> results = response.Result;
                    foreach (SmsBatchPublishWithDiffMessageResult result in results)
                    {
                        Console.WriteLine("messageId={0}, endpoint={1}, code={2}, message={3}",
                            result.MessageId, result.Endpoint, result.Code, result.Message);
                    }
                }
                else
                {
                    Console.WriteLine("code={0}, message={1}, statusCode={2}",
                        response.ErrCode, response.ErrMessage, response.StatusCode);
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 发送短信推广类
        /// </summary>
        public void PromotionSmsPublish()
        {
            // 发送手机号码 号码格式 (+)(国家码)(手机号码)
            List<string> endpoints = new List<string>
            {
                "86136*****87"
            };
            // 设置请求对象
            PromotionSmsPublishRequest request = new PromotionSmsPublishRequest
            {
                Endpoints = endpoints,
                // 短信签名必填,需要在消息通知服务的自助页面申请签名，申请办理时间约2天
                SignId = "47f86cf7c9a7449d98ee61cf193a1060",
                SmsTemplateId = "bfda25c6406e42ddabad74b4a20f6d05"
            };
            try
            {
                // 发送请求并返回响应
                PromotionSmsPublishResponse response = smnClient.SendRequest(request);
                List<PromotionSmsPublishResult> results = response.Result;
                foreach (PromotionSmsPublishResult result in results)
                {
                    Console.WriteLine("messageId={0}, endpoint={1}, code={2}, message={3}",
                        result.MessageId, result.Endpoint, result.Code, result.Message);
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 查询短信签名
        /// </summary>
        public void ListSmsSigns()
        {
            // 设置请求对象
            ListSmsSignsRequest request = new ListSmsSignsRequest();
            try
            {
                // 发送请求并返回响应
                ListSmsSignsResponse response = smnClient.SendRequest(request);
                Console.WriteLine("{0}", Convert.ToString(response));
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
            }
        }

        /// <summary>
        /// 删除短信签名
        /// </summary>
        public void DeleteSmsSign()
        {
            // 设置请求对象
            DeleteSmsSignRequest request = new DeleteSmsSignRequest
            {
                SignId = "741213a6ef644e2a8d977a7f82459f7f",
            };
            try
            {
                // 发送请求并返回响应
                DeleteSmsSignResponse response = smnClient.SendRequest(request);
                Console.WriteLine("{0}", response.RequestId);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
            }
        }

        /// <summary>
        /// 查询短信的发送状态
        /// </summary>
        public void ListSmsMsgReport()
        {
            // 设置请求对象
            ListSmsMsgReportRequest request = new ListSmsMsgReportRequest
            {
                StartTime = "1512625955366",
                EndTime = "1512712355850",
                Limit = 99,
            };
            try
            {
                // 发送请求并返回响应
                ListSmsMsgReportResponse response = smnClient.SendRequest(request);
                Console.WriteLine("{0}", response.Count);
                Console.WriteLine("{0}", response.Data.Count);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
            }
        }

        /// <summary>
        /// 查询已发送短信的内容
        /// </summary>
        public void GetSmsMessage()
        {
            // 设置请求对象
            GetSmsMessageRequest request = new GetSmsMessageRequest
            {
                MessageId = "8cdb2584e1b44246812207ae4c4eaae7",
            };
            try
            {
                // 发送请求并返回响应
                GetSmsMessageResponse response = smnClient.SendRequest(request);
                Console.WriteLine("{0}", response.Message);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
            }
        }

        /// <summary>
        /// 查询短信回调事件
        /// </summary>
        public void ListSmsEvent()
        {
            // 设置请求对象
            ListSmsEventRequest request = new ListSmsEventRequest
            {

            };
            try
            {
                // 发送请求并返回响应
                ListSmsEventResponse response = smnClient.SendRequest(request);
                Console.WriteLine("{0}", response.Callback);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
            }
        }

        /// <summary>
        /// 更新短信回调事件
        /// </summary>
        public void UpdateSmsEvent()
        {
            SmsCallbackRequestData successCallbackData = new SmsCallbackRequestData
            {
                EventType = "sms_success_event",
                TopicUrn = "urn:smn:cn-north-1:cffe4fc4c9a54219b60dbaf7b586e132:sms_event_urn",
            };
            List<SmsCallbackRequestData> listSmsEvents = new List<SmsCallbackRequestData>();
            listSmsEvents.Add(successCallbackData);
            // 设置请求对象
            UpdateSmsEventRequest request = new UpdateSmsEventRequest
            {
                Callback = listSmsEvents,
            };
            try
            {
                // 发送请求并返回响应
                UpdateSmsEventResponse response = smnClient.SendRequest(request);
                Console.WriteLine("{0}", response.RequestId);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
            }
        }

        /// <summary>
        /// 创建短信模板
        /// </summary>
        public void CreateSmsTemplate()
        {
            // 设置请求对象
            CreateSmsTemplateRequest request = new CreateSmsTemplateRequest
            {
                SmsTemplateName = "哈哈哈",
                SmsTemplateContent = "testtyc12020016",
                SmsTemplateType = 1,
                Remark = "test remark"
            };
            try
            {
                // 发送请求并返回响应
                CreateSmsTemplateResponse response = smnClient.SendRequest(request);
                string result = response.SmsTemplateId;
                Console.WriteLine("{0}", result);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 删除短信模板
        /// </summary>
        public void DeleteSmsTemplate()
        {
            // 设置请求对象
            DeleteSmsTemplateRequest request = new DeleteSmsTemplateRequest
            {
                SmsTemplateId = "169a2076213c4553a7ebb3b5ddc07017"
            };
            try
            {
                // 发送请求并返回响应
                DeleteSmsTemplateResponse response = smnClient.SendRequest(request);
                string result = response.RequestId;
                Console.WriteLine("{0}", result);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 查询短信模板列表
        /// </summary>
        public void ListSmsTemplates()
        {
            // 设置请求对象
            ListSmsTemplatesRequest request = new ListSmsTemplatesRequest
            {
                Offset = 0,
                Limit = 10,
                SmsTemplateName = "模板",
                SmsTemplateType = 1
            };
            try
            {
                // 发送请求并返回响应
                ListSmsTemplatesResponse response = smnClient.SendRequest(request);
                Console.WriteLine("count={0}", response.Count);

                foreach (SmsTemplate smsTemplate in response.SmsTemplates)
                {
                    Console.WriteLine("smsTemplateName={0}， smsTemplateType={1}, smsTemplateId={2}, reply={3}," +
                        " status={4}, createTime={5}, validityEndTime={6}", smsTemplate.SmsTemplateName,
                        smsTemplate.SmsTemplateType, smsTemplate.SmsTemplateId, smsTemplate.Reply,
                        smsTemplate.Status, smsTemplate.CreateTime, smsTemplate.ValidityEndTime);
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 查询短信模板详情
        /// </summary>
        public void GetSmsTemplateDetail()
        {
            // 设置请求对象
            GetSmsTemplateDetailRequest request = new GetSmsTemplateDetailRequest
            {
                SmsTemplateId = "bfda25c6406e42ddabad74b4a20f6d05"
            };
            try
            {
                // 发送请求并返回响应
                GetSmsTemplateDetailResponse response = smnClient.SendRequest(request);

                Console.WriteLine("smsTemplateName={0}， smsTemplateType={1}, smsTemplateId={2}, reply={3}," +
                    " status={4}, createTime={5}, validityEndTime={6}, smsTemplateContent={7}", response.SmsTemplateName,
                    response.SmsTemplateType, response.SmsTemplateId, response.Reply,
                    response.Status, response.CreateTime, response.ValidityEndTime, response.SmsTemplateContent);

                Console.ReadLine();
            }
            catch (Exception e)
            {
                // 处理异常
                Console.WriteLine("{0}", e.Message);
                Console.ReadLine();
            }
        }
    }
}
