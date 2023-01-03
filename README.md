# 비접촉식 키오스크 상용화를 위한 시스템 개발
> 기존 접촉식 키오스크에 소프트웨어를 추가해줌으로써 비접촉식 키오스크로의 전환을 달성한다.
  
## 프로젝트 배경 및 목표
### 프로젝트 진행 배경
- 특정 연령층(노인)에게는 키오스크 조작 난이도가 높다는 문제
- 코로나 19로 인한 비접촉 키오스크 기기 전환 과정에서 기존 키오스크를 폐기하며 발생할 수 있는 낭비
- 접촉식 키오스크의 위생 문제
### 프로젝트 목표
- 특정 연령대를 인식하면 키오스크가 보조 모드로 전환하는 기능 
- 키오스크 사용을 돕는, 메뉴 클릭 시 음성 보조 기능
- 모션인식을 통한 비접촉식 키오스크로의 전환

![image](https://user-images.githubusercontent.com/90627763/210287963-6a1ff05c-8267-4909-ae42-e9faaec5315a.png)

## 개발 흐름도
![image](https://user-images.githubusercontent.com/90627763/210289448-35a3104b-2173-4bb3-abd9-028dafdd3b51.png)

## 개발 사항 1 - 사용자 존재 여부 판단 모델
![image](https://user-images.githubusercontent.com/90627763/210289504-a33872bf-4814-49b0-82a6-1cc2044a970e.png)

## 개발 사항 2 - 사용자 연령 판단 모델
![image](https://user-images.githubusercontent.com/90627763/210289534-866f018f-a1aa-42cd-a74e-cc22b626839f.png)
![image](https://user-images.githubusercontent.com/90627763/210289541-c4a92f3a-a72a-4571-bd16-2d8e5fdb6c9a.png)

## 개발 사항 3 - 손동작 판단 모델
### 사용 모델
- mediapipe Handgesture model : 성능 대비 모델의 크기가 작고 간단하며 속도가 빠름 
### 모델 학습
- hyperparameter
  - epoch: 20 | Learning rate: 0.001 | Batch size | Optimizer: Adam | Loss function: CrossEntropy Loss
- 본 프로젝트에 적합한 model 제작하기 위해 다양한 손동작 영상 촬영하여 학습
![image](https://user-images.githubusercontent.com/90627763/210289745-9dfd543b-c745-45c4-a503-78017f482c37.png)

### 실제 분류 손동작
![image](https://user-images.githubusercontent.com/90627763/210289728-8051ffb0-e1d3-4cf7-9683-09aa6e6c4776.png)

## 개발 사항 4 - 키오스크 구현 및 동작 처리
### 구현 과정 
- 시중 키오스크 환경에 맞춰 window OS 환경에서 작동하는 WPF, C# 을 바탕으로 키오스크 구현 (.Net6.0 Framework 사용)
- 카테고리, 메뉴, 장바구니 추가 및 비우기, 결제하기 와 같은 기본적인 키오스크 기능 구현
- 디지털 변화에 적응하기 힘든 노인분들을 위해 일반모드와 구별되는 노인 모드 구현 ( 보조 컴포넌트 실행 + 음성 출력 )
![image](https://user-images.githubusercontent.com/90627763/210289879-48c9a6d7-6507-46b0-b862-db205415ecc0.png)
### 키오스크 메뉴 선택부터 주문 처리 과정
![image](https://user-images.githubusercontent.com/90627763/210289934-584d72a0-f19d-4e9f-8ff3-1518babc6ca0.png)

## 기대 효과 및 활용 방안
### 기대 효과
- COVID-19로 인해 앞당겨진 언택트 시대 흐름에 부합
- 디지털로의 변환이 가속되는 현 시점에서 소외계층 디지털 격차 해소
- 비전을 기반으로 한 모션 사업 분야에서의 활용도 높음
### 활용 방안
- 기술 발전시켜 언택트 시대에 높은 활용도 기대
- 비전 기반 게임 등 다른 사업에도 적용 가능




