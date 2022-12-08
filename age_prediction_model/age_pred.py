import os
os.environ
from PIL import Image
from torchvision import transforms, models
import torch
import torch.nn as nn



model = models.resnet50(pretrained=True)
num_features = model.fc.in_features
model.fc = nn.Linear(num_features, 8)


model_path = './age_prediction_model/age_best_model.pth'
model.load_state_dict(torch.load(model_path, map_location = torch.device('cpu')))

test_transform = transforms.Compose([
    transforms.Resize(128),
    transforms.ToTensor(),
    transforms.Normalize(mean=[0.5, 0.5, 0.5], std=[0.5, 0.5, 0.5])
])

model.eval()
image = Image.open('face.jpg').convert('RGB')
image = test_transform(image).unsqueeze(0)
out = model(image)
_, preds = torch.max(out, 1)
d_class = preds[0].item()

label = -1
if d_class > 4:
    print("노인 입니다")
    os.system("F5.bat")
else:
    print("노인이 아닙니다")
    os.system("F4.bat")
    pass


os.remove('face.jpg')
os.system('python ./hand-gesture-recognition-mediapipe/app.py')
