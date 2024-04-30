<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            padding: 20px;
        }

        .container {
            max-width: 600px;
            margin: 0 auto;
            background-color: #fff;
            border-radius: 5px;
            padding: 20px;
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
            font-weight: bold; /* Initial bold font for title */
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
    </style>
</head>
<body>
    

    <div class="container">
        <div class="row mb-3">
            <div class="col">
                <input id="txtTitle" type="text" value="title" class="fs-2 dynamic-input" runat="server" onclick="showIcons('titleIcons')" />
                <div id="titleIcons" class="dynamic-icon-container">
                    <i id="iconTitleBold" class="fas fa-bold dynamic-icon active" aria-hidden="true" onclick="updateStyle('bold', 'txtTitle', 'iconTitleBold')"></i>
                    <i id="iconTitleItalic" class="fas fa-italic dynamic-icon" aria-hidden="true" onclick="updateStyle('italic', 'txtTitle', 'iconTitleItalic')"></i>
                    <i id="iconTitleUnderline" class="fas fa-underline dynamic-icon" aria-hidden="true" onclick="updateStyle('underline', 'txtTitle', 'iconTitleUnderline')"></i>
                    <i id="iconTitleLink" class="fas fa-link dynamic-icon" aria-hidden="true" onclick="insertLink('txtTitle')"></i>
                    <i id="iconTitleRemoveStyle" class="fas fa-eraser dynamic-icon" aria-hidden="true" onclick="removeStyle('txtTitle', 'iconTitleBold', 'iconTitleItalic', 'iconTitleUnderline')"></i>
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col">
                <input id="txtDescription" type="text" placeholder="Enter description..." class="fs-6 dynamic-input" runat="server" onclick="showIcons('descriptionIcons')" />
                <div id="descriptionIcons" class="dynamic-icon-container">
                    <i id="iconDescriptionBold" class="fas fa-bold dynamic-icon" aria-hidden="true" onclick="updateStyle('bold', 'txtDescription', 'iconDescriptionBold')"></i>
                    <i id="iconDescriptionItalic" class="fas fa-italic dynamic-icon" aria-hidden="true" onclick="updateStyle('italic', 'txtDescription', 'iconDescriptionItalic')"></i>
                    <i id="iconDescriptionUnderline" class="fas fa-underline dynamic-icon" aria-hidden="true" onclick="updateStyle('underline', 'txtDescription', 'iconDescriptionUnderline')"></i>
                    <i id="iconDescriptionLink" class="fas fa-link dynamic-icon" aria-hidden="true" onclick="insertLink('txtDescription')"></i>
                    <i id="iconDescriptionRemoveStyle" class="fas fa-eraser dynamic-icon" aria-hidden="true" onclick="removeStyle('txtDescription', 'iconDescriptionBold', 'iconDescriptionItalic', 'iconDescriptionUnderline')"></i>
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
        function insertLink(inputId) {
            var input = document.getElementById(inputId);
            var url = prompt("Enter URL:");
            if (url && input.value.trim() !== "") {
                var linkName = input.value.trim();  
                input.innerHTML = '<span onclick="window.open(\'' + url + '\', \'_blank\');" style="text-decoration: underline; color: blue; cursor: pointer;">' + linkName + '</span>';
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
    </script>
</body>
</html>
