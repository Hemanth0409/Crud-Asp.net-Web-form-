<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuizForms.aspx.cs" Inherits="Crud__Asp.net_Web_form_.QuizForms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        #progress-bar-container {
            position: relative;
            width: 90%;
            margin: auto;
            margin-top: 65px;
        }

        #progress-bar-container ul {
            padding-top: 15px;
            list-style: none;
            display: flex;
            justify-content: space-between;
        }

        #progress-bar-container li {
            flex: 1;
            text-align: center;
            color: #aaa;
            text-transform: uppercase;
            font-size: 11px;
            cursor: pointer;
            font-weight: 700;
            position: relative;
        }

        #progress-bar-container li.active::before,
        #progress-bar-container li:hover::before {
            border: 2px solid #fff;
            background-color: crimson;
        }

        #progress-bar-container li::before {
            content: attr(data-step);
            display: block;
            margin: auto;
            width: 30px;
            height: 30px;
            line-height: 30px;
            border-radius: 50%;
            border: 2px solid #aaa;
            transition: all ease 0.3s;
            position: absolute;
            top: -15px;
            left: 50%;
            transform: translateX(-50%);
        }

        #progress-bar-container li.step1::before {
            left: 0;
        }

        #progress-bar-container li.step4::before {
            right: 0;
        }

        #progress-bar-container #line {
            width: 100%;
            margin: auto;
            background-color: #eee;
            height: 6px;
            position: absolute;
            left: 0;
            top: 7px;
            z-index: 1;
            border-radius: 50px;
            transition: all ease 0.75s;
        }

        #progress-bar-container #line-progress {
            content: " ";
            width: 8%;
            height: 100%;
            background-color: #207893;
            background: linear-gradient(to right, #207893 0%, #2ea3b7 100%);
            position: absolute;
            z-index: 2;
            border-radius: 50px;
            transition: 0.6s cubic-bezier(0.68, -0.55, 0.265, 1.25);
        }

        #progress-content-section {
            position: relative;
            top: 100px;
            width: 90%;
            margin: auto;
            background: #f3f3f3;
            border-radius: 4px;
        }

        .section-content {
            padding: 30px 40px;
            text-align: center;
            display: none;
        }

        .section-content.active {
            display: block;
        }

        .progress-wrapper {
            margin: auto;
            max-width: 1080px;
        }

        @media only screen and (max-width: 768px) {
            #progress-bar-container ul {
                flex-direction: column;
            }

            #progress-bar-container li {
                width: 100%;
                margin-bottom: 15px;
            }

            #progress-bar-container li::before {
                top: -25px;
            }
        }
    </style>

    <script>
        $(document).ready(function () {
            $(".step").click(function () {
                $(this).addClass("active").prevAll().addClass("active");
                $(this).nextAll().removeClass("active");
            });

            $(".step01").click(function () {
                $("#line-progress").css("width", "8%");
                $(".step1").addClass("active").siblings().removeClass("active");
            });

            $(".step02").click(function () {
                $("#line-progress").css("width", "50%");
                $(".step2").addClass("active").siblings().removeClass("active");
            });

            $(".step03").click(function () {
                $("#line-progress").css("width", "100%");
                $(".step3").addClass("active").siblings().removeClass("active");
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="progress-wrapper">
            <div id="progress-bar-container">
                <ul>
                    <li class="step step01 active" data-step="1">
                        <div class="step-inner">Step 1</div>
                    </li>
                    <li class="step step02" data-step="2">
                        <div class="step-inner">Step 2</div>
                    </li>
                    <li class="step step03" data-step="3">
                        <div class="step-inner">Step 3</div>
                    </li>
                   
                </ul>
                <div id="line">
                    <div id="line-progress"></div>
                </div>                
                <div id="progress-content-section">
                    <div class="section-content step1 active">
                        <h2>Step 1</h2>
                        <p>
                            Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellat,
                            impedit! Lorem ipsum dolor sit amet consectetur, adipisicing elit. Necessitatibus, soluta impedit. Eligendi aliquam ratione porro minus temporibus facilis iure numquam.
                        </p>
                    </div>
                    <div class="section-content step2">
                        <h2>Step 2</h2>
                        <p>
                            Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellat,
                            impedit! Lorem ipsum dolor sit amet consectetur, adipisicing elit. Necessitatibus, soluta impedit. Eligendi aliquam ratione porro minus temporibus facilis iure numquam.
                            Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellat,
                            impedit! Lorem ipsum dolor sit amet consectetur, adipisicing elit. Necessitatibus, soluta impedit. Eligendi aliquam ratione porro minus temporibus facilis iure numquam.
                        </p>
                    </div>
                    <div class="section-content step3">
                        <h2>Step 3</h2>
                        <p>
                            Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellat,
                            impedit! Lorem ipsum dolor sit amet consectetur, adipisicing elit. Necessitatibus, soluta impedit. Eligendi aliquam ratione porro minus temporibus facilis iure numquam.
                            Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellat,
                            impedit! Lorem ipsum dolor sit amet consectetur, adipisicing elit. Necessitatibus, soluta impedit. Eligendi aliquam ratione porro minus temporibus facilis iure numquam.
                            Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellat,
                            impedit! Lorem ipsum dolor sit amet consectetur, adipisicing elit. Necessitatibus, soluta impedit. Eligendi aliquam ratione porro minus temporibus facilis iure numquam.
                        </p>
                    </div>
                     
                </div>
            </div>        
        </div>
    </form>
</body>
</html>
