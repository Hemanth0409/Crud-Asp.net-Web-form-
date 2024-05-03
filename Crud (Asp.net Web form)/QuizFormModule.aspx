<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.0/css/all.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            padding: 20px;
        }

        .displayContainer {
            background-color: #fff;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .toolBox {
            max-width: 50px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }


        .dynamic-input {
            margin-bottom: 10px;
            padding: 8px;
            width: calc(100% - 32px);
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 16px;
            transition: border-color 0.3s ease;
            font-weight: bold;
        }

            .dynamic-input:focus {
                outline: none;
                border-color: #007bff;
            }

        .dynamic-icon {
            margin-right: 10px;
            cursor: pointer;
            color: #777;
            transition: color 0.3s ease;
        }

            .dynamic-icon.active {
                color: #007bff;
            }

        .dynamic-icon-container {
            display: none;
        }

            .dynamic-icon-container.show {
                display: inline-block;
            }

        .sidebar {
            position: absolute;
            top: 0;
            right: -250px;
            width: 250px;
            height: 100%;
            background-color: #f8f9fa;
            transition: right 0.3s ease;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            z-index: 999;
        }

            .sidebar.show {
                right: 0;
            }
    </style>
</head>

<body>
    <div class="container displayContainer">
        <div class="row mb-3">
            <div class="col">
                <input id="txtTitle" type="text" value="title" class="fs-2 dynamic-input" runat="server"
                    onclick="showIcons('titleIcons')" />
                <div id="titleIcons" class="dynamic-icon-container">
                    <i id="iconTitleBold" class="fas fa-bold dynamic-icon active" aria-hidden="true"
                        onclick="updateStyle('bold', 'txtTitle', 'iconTitleBold')"></i>
                    <i id="iconTitleItalic" class="fas fa-italic dynamic-icon" aria-hidden="true"
                        onclick="updateStyle('italic', 'txtTitle', 'iconTitleItalic')"></i>
                    <i id="iconTitleUnderline" class="fas fa-underline dynamic-icon" aria-hidden="true"
                        onclick="updateStyle('underline', 'txtTitle', 'iconTitleUnderline')"></i>
                    <i id="iconTitleRemoveStyle" class="fas fa-eraser dynamic-icon" aria-hidden="true"
                        onclick="removeStyle('txtTitle', 'iconTitleBold', 'iconTitleItalic', 'iconTitleUnderline')"></i>
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col">
                <input id="txtDescription" type="text" placeholder="Enter description..." class="fs-6 dynamic-input"
                    runat="server" onclick="showIcons('descriptionIcons')" />
                <div id="descriptionIcons" class="dynamic-icon-container">
                    <i id="iconDescriptionBold" class="fas fa-bold dynamic-icon active" aria-hidden="true"
                        onclick="updateStyle('bold', 'txtDescription', 'iconDescriptionBold')"></i>
                    <i id="iconDescriptionItalic" class="fas fa-italic dynamic-icon" aria-hidden="true"
                        onclick="updateStyle('italic', 'txtDescription', 'iconDescriptionItalic')"></i>
                    <i id="iconDescriptionUnderline" class="fas fa-underline dynamic-icon" aria-hidden="true"
                        onclick="updateStyle('underline', 'txtDescription', 'iconDescriptionUnderline')"></i>
                    <i id="iconDescriptionRemoveStyle" class="fas fa-eraser dynamic-icon" aria-hidden="true"
                        onclick="removeStyle('txtDescription', 'iconDescriptionBold', 'iconDescriptionItalic', 'iconDescriptionUnderline')"></i>
                </div>
            </div>
        </div>
    </div>
    <div class="container  mt-5 d-flex ">
        <div class="displayContainer col-12">
            <div class="row mb-2 justify-content-evenly">
                <div class="col-5">
                    <input id="formQuestions" type="text" placeholder="Question" class="fs-6 dynamic-input"
                        runat="server" />
                </div>
                <div class="col-2 justify-content-center">
                    <i class="fas fa-upload dynamic-icon" aria-hidden="true"></i>
                </div>
                <div class="col-5">
                    <select id="formQuestionType" class="form-select fs-6">
                        <option value="multiple">Multiple Select</option>
                        <option value="checkbox">Checkbox</option>
                        <option value="radio">Radio Button</option>
                    </select>
                </div>
            </div>
            <div class="row mb-1">
                <div class="col-12 mt-3">
                    <div class="form-check">
                        <input class="form-check-input" type="radio" id="option1" />
                        <label class="form-check-label" for="option1">
                            Option 1
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" id="option2" />
                        <label class="form-check-label" for="option2">
                            Option 2
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" id="option3" />
                        <label class="form-check-label" for="option3">
                            Option 3
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" id="option4" />
                        <label class="form-check-label" for="option4">
                            Option 4
                        </label>
                    </div>
                </div>
            </div>
        </div>
        <div class="container toolBox ms-3 col-md-2">
            <div class="row d-flex flex-column justify-content-center align-items-center pt-3 pb-3">
                <div class="col-12 mb-3">
                    <i class="fa-solid fa-folder-plus fa-lg dynamic-icon" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Add Questions"></i>
                </div>
                <div class="col-12 mb-3">
                    <i class="fa-solid fa-image fa-lg dynamic-icon" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Add Image"></i>
                </div>
                <div class="col-12 mb-3">
                    <i class="fa-solid fa-video fa-lg dynamic-icon" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Add Video"></i>
                </div>
                <div class="col-12">
                    <i class="fa-solid fa-t fa-lg dynamic-icon" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Add Title"></i>
                </div>
                <div class="col-12 mt-3">
                    <i class="fa-solid fa-trash-can fa-lg dynamic-icon" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Delete Question"></i>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        window.onload = function () {
            updateStyle('bold', 'txtTitle', 'iconTitleBold');
        };

        function showIcons(iconDivId) {
            var allIconContainers = document.querySelectorAll('.dynamic-icon-container');
            allIconContainers.forEach(function (container) {
                container.classList.remove('show');
            });
            document.getElementById(iconDivId).classList.add('show');
        }

        function updateStyle(fontStyle, inputId, iconId) {
            var input = document.getElementById(inputId);
            var icon = document.getElementById(iconId);
            switch (fontStyle) {
                case 'bold':
                    input.style.fontWeight = input.style.fontWeight === 'bold' ? 'normal' : 'bold';
                    icon.classList.toggle('active', input.style.fontWeight === 'bold');
                    break;
                case 'italic':
                    input.style.fontStyle = input.style.fontStyle === 'italic' ? 'normal' : 'italic';
                    icon.classList.toggle('active', input.style.fontStyle === 'italic');
                    break;
                case 'underline':
                    input.style.textDecoration = input.style.textDecoration === 'underline' ? 'none' : 'underline';
                    icon.classList.toggle('active', input.style.textDecoration === 'underline');
                    break;
                default:
                    break;
            }
        }

        function removeStyle(inputId, ...iconIds) {
            var input = document.getElementById(inputId);
            input.style.fontWeight = 'normal';
            input.style.fontStyle = 'normal';
            input.style.textDecoration = 'none';
            iconIds.forEach(function (iconId) {
                var icon = document.getElementById(iconId);
                icon.classList.remove('active');
            });
        }

        document.body.addEventListener('click', function (event) {
            var isInputField = event.target.tagName === 'INPUT';
            var isIconContainer = event.target.classList.contains('dynamic-icon');
            if (!isInputField && !isIconContainer) {
                var allIconContainers = document.querySelectorAll('.dynamic-icon-container');
                allIconContainers.forEach(function (container) {
                    container.classList.remove('show');
                });
            }
        });
        function optionSideBar() {
            var sidebar = document.getElementById('sidebar');
            sidebar.classList.toggle('show');
        }
    </script>
</body>
</html>
