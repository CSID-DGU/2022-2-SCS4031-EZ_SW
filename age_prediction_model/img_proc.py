import cv2
from torchvision.transforms import ToTensor
from torchvision import transforms

def img_proc(path):
    
    img = cv2.imread(path, cv2.IMREAD_UNCHANGED)
    resized_img = cv2.resize(img, (128,128), interpolation = cv2.INTER_AREA)
    transform = transforms.ToTensor()
    tensor = transform(resized_img)
    
    return tensor