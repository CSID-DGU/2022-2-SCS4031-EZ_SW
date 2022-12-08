import time

import cv2
import mediapipe as mp
mp_face_detection = mp.solutions.face_detection
import os

os.environ['OPENCV_FFMPEG_CAPTURE_OPTIONS'] = 'rtsp_transport;udp'
class face_detection:
	def __init__(self):
		self.URL = "rtsp://192.168.0.22:8554/"
		self.cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)
		self.image = None
		self.device = 0
		self.width = 960
		self.height = 540
	def detect_face(self):
		cap = self.cap
		cap.set(cv2.CAP_PROP_FRAME_WIDTH, self.width)
		cap.set(cv2.CAP_PROP_FRAME_HEIGHT, self.height)
		while 1:
			ret, frame = cap.read()
			self.image = frame

			with mp_face_detection.FaceDetection(model_selection=1, min_detection_confidence=1) as face_detection:
				results = face_detection.process(cv2.cvtColor(self.image, cv2.COLOR_BGR2RGB))

				if not results.detections:  # face detect X -> return False
					pass	
				else:  # detected face -> return True
					cv2.imwrite("face.jpg", frame)
					cap.release()
					return True


if __name__ == '__main__':
	fd = face_detection()

	if fd.detect_face():

		os.system("F3.bat")
		time.sleep(2);
		print("2초간 사람 인식 완료")
		os.system('python ./age_prediction_model/age_pred.py')



