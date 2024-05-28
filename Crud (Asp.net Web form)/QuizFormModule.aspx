﻿<%@ Page Language="C#" AutoEventWireup="true" EnableSessionState="True" CodeBehind="QuizFormModule.aspx.cs" Inherits="Crud__Asp.net_Web_form_.QuizFormModule" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.0/css/all.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
            overflow-x: hidden;
        }

        .displayContainer {
            background-color: #fff;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            overflow-x: hidden;
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

        input[type="checkbox"] {
            position: relative;
            width: 40px;
            height: 25px;
            -webkit-appearance: none;
            appearance: none;
            background: red;
            outline: none;
            border-radius: 2rem;
            cursor: pointer;
            box-shadow: inset 0 0 5px rgb(0 0 0 / 50%);
        }

        .questionContainer.checked {
            border-color: #00ed64;
            box-shadow: 0 0 20px rgba(0, 237, 100, 0.5);
        }

        input[type="checkbox"]::before {
            content: "";
            width: 17px;
            height: 17px;
            border-radius: 50%;
            background: #fff;
            position: absolute;
            top: 2px;
            left: 2px;
            transition: 0.5s;
        }

        input[type="checkbox"]:checked::before {
            transform: translateX(100%);
            background: #fff;
            right: 4px;
        }

        input[type="checkbox"]:checked {
            background: #00ed64;
        }
    </style>
</head>

<body>
    <form id="quizFormModuleId" runat="server">
        <div class="container displayContainer" id="titleDisplayContainer" onclick="showToolBox(this)">
            <div class="row mb-3">
                <div class="col">
                    <input id="txtTitle" type="text" value="title" placeholder="title " class="fs-2 dynamic-input" runat="server" onchange="checkTitleExists(this.value)"
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
        <div>
            <asp:HiddenField ID="hiddenJsonData" runat="server" />
        </div>
        <div id="questionPlaceholder" runat="server"></div>

        <div id="questionTemplate" runat="server" style="display: none;">
            <div class="container mt-5 d-flex questionContainer">
                <div class="displayContainer col-12">
                    <div class="row mb-2 justify-content-between align-items-center">
                        <div class="col-5 d-flex align-items-center" id="questionContainer">
                            <span class="question-number me-2"></span>
                            <textarea placeholder="Question" runat="server" class="fs-6 dynamic-input" id="formQuestion"></textarea>
                        </div>
                        <div class="col-1 text-center" id="questionImageContainer">
                            <i class="fas fa-upload dynamic-icon" aria-hidden="true"></i>
                        </div>
                        <div class="col-3 text-center" id="toggleContainer">
                            <label>Required</label>
                            <input type="checkbox" runat="server" id="isRequiredId" class="required-toggle" onclick="toggleClick(this)" />
                        </div>
                        <div class="col-3" id="fieldTypeContainer">
                            <select id="ddlQuestionType" runat="server" class="form-select fs-6">
                                <option value="1">Multiple Choice</option>
                                <option value="2">Checkbox</option>
                                <option value="3">Drop Down</option>
                            </select>
                        </div>
                    </div>
                    <span class="dropdown-value" id="dropdownValue"></span>
                    <div class="row mb-1" runat="server" id="optionsDisplay"></div>
                    <div class="d-flex align-items-center">
                        <button class="btn btn-sm btn-primary ms-4 mt-5 add-answer" onclick="addAnswer(this.closest('.questionContainer'))">Add Answer</button>
                        <button class="btn btn-sm btn-primary ms-auto mt-5 done" style="visibility: hidden" onclick="confirmOptionClick(this.closest('.questionContainer'))">Done</button>
                    </div>
                </div>
                <div class="container toolBox ms-3 col-md-2" id="toolBoxDisplay">
                    <div class="row d-flex flex-column justify-content-center align-items-center gap-3">
                        <div class="col-12 mt-4">
                            <i class="fa-solid fa-folder-plus fa-lg dynamic-icon" aria-hidden="true" onclick="addQuestion()"></i>
                        </div>
                        <div class="col-12 mt-3">
                            <i class="fa-solid fa-image fa-lg dynamic-icon" aria-hidden="true"
                                data-toggle="tooltip" data-placement="right" title="Add Image for question"></i>
                        </div>
                        <div class="col-12 mt-3">
                            <i class="fa-solid fa-video fa-lg dynamic-icon" aria-hidden="true"
                                data-toggle="tooltip" data-placement="right" title="Add Video for question"></i>
                        </div>
                        <div class="col-12 mt-3">
                            <i class="fa-solid fa-t fa-lg dynamic-icon" aria-hidden="true"
                                data-toggle="tooltip" data-placement="right" title="Add Title"></i>
                        </div>
                        <div class="col-12 mt-3 mb-2">
                            <i class="fa-solid fa-trash-can fa-lg dynamic-icon" aria-hidden="true"
                                data-toggle="tooltip" data-placement="right" title="Delete Question"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 mt-3">
            <asp:Button runat="server" class="btn-primary  ms-auto" Text="Submit" OnClick="submitFormClick" OnClientClick="return submitForm();" />
        </div>
        <asp:HiddenField ID="jsonDataField" runat="server" />
    </form>
    <script type="text/javascript">

        var titleExists = false;

        function checkTitleExists(title) {
            PageMethods.IsTitleExists(title, function (response) {
                if (response) {
                    alert("The title already exists. Please choose a different title.");
                    document.getElementById('txtTitle').focus();
                }
            }, function (error) {
                console.error("Error checking title existence: " + error.get_message());
            });
        }

        window.onload = function () {
            updateStyle('bold', 'txtTitle', 'iconTitleBold');
            addQuestion();
            var jsonData = document.getElementById('<%= hiddenJsonData.ClientID %>').value;
            try {
                var responseData = JSON.parse(jsonData);
                if (responseData && responseData.questions) {
                    document.getElementById('txtTitle').value = responseData.title;
                    document.getElementById('txtDescription').value = responseData.description;

                    var questionPlaceholder = document.getElementById('questionPlaceholder');

                    responseData.questions.forEach(function (question, index) {
                        var newQuestionContainer = document.createElement('div');
                        newQuestionContainer.classList.add('questionContainer');
                        newQuestionContainer.setAttribute('data-question-id', index + 1);
                        newQuestionContainer.innerHTML = document.getElementById('questionTemplate').innerHTML;
                        newQuestionContainer.querySelector('.question-number').textContent = "[" + (index + 1) + "].";

                        newQuestionContainer.querySelector('.dynamic-input').value = question.questionText;

                        var requiredToggle = newQuestionContainer.querySelector('.required-toggle');
                        requiredToggle.checked = question.isRequired;

                        var fieldTypeDropdown = newQuestionContainer.querySelector('.form-select');
                        fieldTypeDropdown.value = question.questionType;

                        const inputFieldType = fieldTypeDropdown.value === '1' ? 'radio' : 'checkbox';
                        var optionsDisplay = newQuestionContainer.querySelector('#optionsDisplay');
                        question.options.forEach(function (option, optionIndex) {
                            var newOption = document.createElement('div');
                            newOption.classList.add('col-md-11', 'mt-3');
                            newOption.innerHTML = `
                    <div class="form-check d-flex align-items-center">
                        <input class="form-check-input me-2 option-checkbox" name="question${index + 1}" title="fill the option if correct" type="${inputFieldType}" id="checkbox${optionIndex + 1}" onchange="toggleOptionBackground(this)">
                        <input type="text" class="form-control border-bottom flex-grow-1 option-input" id="option${optionIndex + 1}Input" value="${option.text}" />
                        <i class="fas fa-image fa-lg dynamic-icon image-icon ms-1" aria-hidden="true" onclick="addImage(this)" title="Add image for options "></i>
                        <i class="fas fa-trash fa-lg dynamic-icon delete-icon ms-1" aria-hidden="true" style="display: none;" onclick="deleteOption(this)" title="Delete the option"></i>
                    </div>`;
                            optionsDisplay.appendChild(newOption);
                        });
                        questionPlaceholder.appendChild(newQuestionContainer);
                    });
                } else {
                    console.error("responseData or responseData.questions is undefined");
                }
            } catch (error) {
                console.error("Error parsing JSON data:", error);
            }
        };

        function collectFormData() {
            var formData = {
                title: document.getElementById('txtTitle').value,
                description: document.getElementById('txtDescription').value,
                questions: []
            };
            var questionContainers = document.querySelectorAll('.questionContainer');
            var questionTexts = [];
            questionContainers.forEach(function (questionContainer) {
                var questionText = questionContainer.querySelector('.dynamic-input').value.trim();
                if (questionText !== '') {
                    if (questionTexts.indexOf(questionText) === -1) {
                        var question = {
                            questionText: questionText,
                            questionType: questionContainer.querySelector('.form-select').value,
                            isRequired: questionContainer.querySelector('.required-toggle').checked,
                            options: []
                        };
                        var optionInputs = questionContainer.querySelectorAll('.form-check-input');
                        optionInputs.forEach(function (optionInput) {
                            question.options.push({
                                id: optionInput.id,
                                text: optionInput.nextElementSibling.value
                            });
                        });
                        formData.questions.push(question);
                        questionTexts.push(questionText);
                    }
                }
            });
            return formData;
        }

        function toggleClick(checkbox) {
            const isChecked = checkbox.checked;
            const questionContainer = checkbox.closest('.questionContainer');
            const isRequired = questionContainer.dataset.isrequired === 'True';
            questionContainer.querySelector('input[type="text"]').required = isRequired && isChecked;
            if (isChecked) {
                questionContainer.classList.add('checked');
            } else {
                questionContainer.classList.remove('checked');
            }
        }

        function submitForm() {
            var formData = collectFormData();
            var jsonData = JSON.stringify(formData);
            console.log(jsonData);
            document.getElementById('<%= jsonDataField.ClientID %>').value = jsonData;
        }

        function showIcons(iconDivId) {
            var allIconContainers = document.querySelectorAll('.dynamic-icon-container');
            allIconContainers.forEach(function (container) {
                container.classList.remove('show');
                console.log(allIconContainers + 'Removed');
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

        function showToolBox(container) {
            var toolBoxDisplay = container.querySelector('.toolBox');
            if (toolBoxDisplay) {
                var allToolBoxes = document.querySelectorAll('.toolBox');
                allToolBoxes.forEach(function (toolbox) {
                    toolbox.style.display = "none";
                });
                toolBoxDisplay.style.display = "block";
            }
        }

        let questionCount = 1;

        function addNewOption(questionContainer, optionCount) {
            const optionsContainer = questionContainer.querySelector('.row#optionsDisplay');
            const existingOptions = optionsContainer.querySelectorAll('.col-md-11');
            if (optionsContainer) {
                const newOption = document.createElement('div');
                newOption.classList.add('col-md-11', 'mt-3');
                const dropdownValue = questionContainer.querySelector('.form-select').value;
                const inputFieldType = dropdownValue === '1' ? 'radio' : 'checkbox';
                const questionId = questionContainer.getAttribute('data-question-id');
                newOption.innerHTML = `
            <div class="form-check d-flex align-items-center">
                <input class="form-check-input me-2 option-checkbox" name="question${questionId}" title="fill the option if correct" type="${inputFieldType}" id="checkbox${optionCount}" onchange="toggleOptionBackground(this)">
                <input type="text" class="form-control border-bottom flex-grow-1 option-input"   id="option${optionCount}Input" value="Option ${optionCount}" />
                <i class="fas fa-image fa-lg dynamic-icon image-icon ms-1" aria-hidden="true" onclick="addImage(this)" title="Add image for options "></i>
                <i class="fas fa-trash fa-lg dynamic-icon delete-icon ms-1" aria-hidden="true" style="display: none;" onclick="deleteOption(this)" title="Delete the option"></i>
            </div>`;
                if (existingOptions.length === 0) {
                    newOption.querySelector('.delete-icon').style.display = 'none';
                }
                optionsContainer.appendChild(newOption);
                optionCount++;
                const optionInputs = newOption.querySelectorAll('.form-check-input');
                optionInputs.forEach(function (optionInput) {
                    optionInput.disabled = true;
                });
                if (existingOptions.length > 0) {
                    const previousOption = existingOptions[existingOptions.length - 1];
                    const plusIcon = previousOption.querySelector('.plus-icon');
                    if (plusIcon) {
                        plusIcon.style.display = 'none';
                    }
                    const deleteIcon = previousOption.querySelector('.delete-icon');
                    deleteIcon.style.display = 'inline-block';
                }
                const plusIcon = document.createElement('i');
                plusIcon.classList.add('fas', 'fa-plus-circle', 'ms-2', 'dynamic-icon', 'plus-icon');
                plusIcon.setAttribute('aria-hidden', 'true');
                plusIcon.setAttribute('title', 'Add options');
                plusIcon.onclick = function () {
                    addNewOption(questionContainer, optionCount);
                };
                newOption.querySelector('.form-check').appendChild(plusIcon);
            } else {
                console.error("Options container not found!.");
            }
        }

        function addQuestion() {
            const newQuestionContainer = document.createElement('div');
            newQuestionContainer.classList.add('questionContainer');
            newQuestionContainer.setAttribute('data-question-id', questionCount);
            newQuestionContainer.innerHTML = document.getElementById('questionTemplate').innerHTML;
            newQuestionContainer.querySelector('.question-number').textContent = "[" + questionCount + "].";
            const dropdowns = newQuestionContainer.querySelectorAll('.form-select');
            dropdowns.forEach(function (dropdown) {
                dropdown.id += questionCount;
                dropdown.addEventListener('change', function () {
                    updateOptionTypes(newQuestionContainer, dropdown.value);
                });
                const initialDropdownValue = dropdown.options[dropdown.selectedIndex].value;
                updateOptionTypes(newQuestionContainer, initialDropdownValue);
            });
            const deleteIcon = newQuestionContainer.querySelector('.fa-trash-can');
            deleteIcon.addEventListener('click', function () {
                if (document.querySelectorAll('.questionContainer').length > 1) {
                    newQuestionContainer.remove();
                } else {
                    alert("At least one template should remain.");
                }
            });

            newQuestionContainer.addEventListener('click', function (event) {
                const allToolBoxes = document.querySelectorAll('.toolBox');
                allToolBoxes.forEach(function (toolbox) {
                    toolbox.style.display = "none";
                });
                const toolBoxDisplay = this.querySelector('.toolBox');
                toolBoxDisplay.style.display = "block";
                event.stopPropagation();
            });

            let optionCount = 1;
            addNewOption(newQuestionContainer, optionCount);
            document.body.appendChild(newQuestionContainer);
            questionCount++;
            window.scrollTo(0, document.body.scrollHeight);
        }

        function updateOptionTypes(questionContainer, selectedValue) {
            const inputFieldType = selectedValue === '1' ? 'radio' : 'checkbox';
            const questionId = questionContainer.getAttribute('data-question-id');
            const options = questionContainer.querySelectorAll('.option-checkbox');
            options.forEach(function (option) {
                option.type = inputFieldType;
                option.name = "question" + questionId;
            });
        }

        function confirmOptionClick(container) {
            const addAnswerBtn = container.querySelector('.btn-primary.add-answer');
            addAnswerBtn.style.display = 'block';

            const requiredToggle = container.querySelector("#toggleContainer");
            requiredToggle.style.display = 'block';

            const questionImage = container.querySelector("#questionImageContainer");
            questionImage.style.display = 'block';

            const fieldType = container.querySelector("#fieldTypeContainer");
            fieldType.style.display = 'block';

            const questionContainer = container.querySelector("#questionContainer");
            questionContainer.classList = "d-flex col-4";

            const doneBtn = container.querySelector('.btn-primary.done');
            doneBtn.style.visibility = 'hidden basic';
            doneBtn.style.display = 'none';

            const options = container.querySelectorAll('.form-check-input');
            options.forEach(function (option) {
                option.disabled = true;
            });

            const inputFields = container.querySelectorAll('.form-control');
            inputFields.forEach(function (inputField) {
                inputField.classList.remove('required');
            });
        }

        function addAnswer(container) {
            const addAnswerBtn = container.querySelector('.btn-primary.add-answer');
            addAnswerBtn.style.display = 'none';
            const doneBtn = container.querySelector('.btn-primary.done');
            doneBtn.style.visibility = 'visible';
            doneBtn.style.display = 'block';
            const questionImage = container.querySelector("#questionImageContainer");
            questionImage.style.display = 'none';

            const requiredToggle = container.querySelector("#toggleContainer");
            requiredToggle.style.display = 'none';

            const questionContainer = container.querySelector("#questionContainer");
            questionContainer.classList = "d-flex col-10";

            const fieldType = container.querySelector("#fieldTypeContainer");
            fieldType.style.display = 'none';

            const options = container.querySelectorAll('.form-check-input');
            options.forEach(function (option) {
                option.disabled = false;
            });


            const inputFields = container.querySelectorAll('.form-control');
            inputFields.forEach(function (inputField) {
                inputField.classList.add('required');
            });
            const ddlQuestionType = container.querySelector('.form-select');
            const selectedValue = ddlQuestionType.value;
            console.log("Selected question type:", selectedValue);
        }

        function toggleOptionBackground(checkbox) {
            var inputField = checkbox.nextElementSibling;
            if (checkbox.checked) {
                inputField.style.borderColor = 'lightgreen';
                inputField.style.boxShadow = '0 0 5px lightgreen';
            } else {
                inputField.style.borderColor = '';
                inputField.style.boxShadow = '';
            }
        }

        function deleteOption(element) {
            element.closest('.col-md-11').remove();
        }
    </script>
</body>
</html>
