import cv2
import mediapipe as mp

mp_face_detection = mp.solutions.face_detection

class face_detection:
    def __init__(
        self,
        image_path = None
    ):
        self.image_path = image_path
        self.image = None
    
    def get_image(self):
        self.image = cv2.imread(self.image_path)
    
    def detect_face(self):
        with mp_face_detection.FaceDetection(model_selection=1, min_detection_confidence=1) as face_detection:
            results = face_detection.process(cv2.cvtColor(self.image, cv2.COLOR_BGR2RGB))
            
            if not results.detections: # face detect X -> return False
                return False
            else: # detected face -> return True
                return True
            
 