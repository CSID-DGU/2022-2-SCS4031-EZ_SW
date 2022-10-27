import torch
import torch.nn as nn


class CNN(nn.Module):
    def __init__(self):
        super(CNN, self).__init__()
        self.conv_1 = self._con_dw_sep(3, 16)
        self.conv_2 = self._con_dw_sep(16, 32)
        self.conv_3 = self._con_dw_sep(32, 64)
        
        self.fc1 = nn.Linear(10816, 512)
        self.fc2 = nn.Linear(512, 1)
        
        self.dropout = nn.Dropout(0.5)
        self.relu = nn.ReLU()
        self.sigmoid = nn.Sigmoid()
        
    def _con_dw_sep(self, C_in, C_out):
        conv_layer = nn.Sequential(
            nn.Conv2d(C_in, C_in, kernel_size = 4, groups=C_in),
            nn.Conv2d(C_in, C_out , kernel_size=1),
            nn.ReLU(),
            nn.MaxPool2d(kernel_size=2, stride=2)
        )
        return conv_layer
    
    def forward(self, x):
        out = self.conv_1(x)
        out = self.conv_2(out)
        out = self.conv_3(out)
    
        out = out.view(-1, 10816)
        
        out = self.dropout(out)
        out = self.fc1(out)
        out = self.relu(out)
        
        out = self.dropout(out)
        out = self.fc2(out)
        
        out = out.squeeze()
        out = self.sigmoid(out)
        
        return out.float()
        